using AbstractGiftShopModel;
using AbstractGiftShopServiceDAL.BindingModels;
using AbstractGiftShopServiceDAL.Interfaces;
using AbstractGiftShopServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace AbstractGiftShopServiceImplementDataBase.Implementations
{
    public class MainServiceDB : IMainService
    {
        private AbstractGiftShopDbContext context;
        public MainServiceDB(AbstractGiftShopDbContext context)
        {
            this.context = context;
        }
        public List<SOrderViewModel> GetList()
        {
            List<SOrderViewModel> result = context.SOrders.Select(rec => new SOrderViewModel
            {
                Id = rec.Id,
                SClientId = rec.SClientId,
                GiftId = rec.GiftId,
                ImplementerId = rec.ImplementerId,
                DateCreate = SqlFunctions.DateName("dd", rec.DateCreate) + " " +
            SqlFunctions.DateName("mm", rec.DateCreate) + " " +
            SqlFunctions.DateName("yyyy", rec.DateCreate),
                DateImplement = rec.DateImplement == null ? "" :
            SqlFunctions.DateName("dd",
           rec.DateImplement.Value) + " " +
            SqlFunctions.DateName("mm",
           rec.DateImplement.Value) + " " +
            SqlFunctions.DateName("yyyy",
           rec.DateImplement.Value),
                Status = rec.Status.ToString(),
                Count = rec.Count,
                Sum = rec.Sum,
                SClientFIO = rec.SClient.SClientFIO,
                GiftName = rec.Gift.GiftName,
                ImplementerFIO = rec.Implementer.ImplementerFIO
            })
            .ToList();
            return result;
        }
        public List<SOrderViewModel> GetFreeOrders()
        {
            List<SOrderViewModel> result = context.SOrders
            .Where(x => x.Status == SOrderStatus.Принят || x.Status ==
          SOrderStatus.НедостаточноРесурсов)
            .Select(rec => new SOrderViewModel
            {
                Id = rec.Id
            })
            .ToList();
            return result;
        }
        public void CreateOrder(SOrderBindingModel model)
        {
            var order = new SOrder
            {
                SClientId = model.SClientId,
                GiftId = model.GiftId,
                DateCreate = DateTime.Now,
                Count = model.Count,
                Sum = model.Sum,
                Status = SOrderStatus.Принят
            };
        context.SOrders.Add(order);
            context.SaveChanges();
            var client = context.SClients.FirstOrDefault(x => x.Id == model.SClientId);
            SendEmail(client.Mail, "Оповещение по заказам", string.Format("Заказ №{0} от {1} создан успешно", order.Id, order.DateCreate.ToShortDateString()));
        }
        public void TakeOrderInWork(SOrderBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                SOrder element = context.SOrders.FirstOrDefault(rec => rec.Id == model.Id);
                try
                {
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    if (element.Status != SOrderStatus.Принят && element.Status !=
                    SOrderStatus.НедостаточноРесурсов)
                    {
                        throw new Exception("Заказ не в статусе \"Принят\"");
                    }
                    var giftMaterials = context.GiftMaterialss.Include(rec =>
                    rec.Materials).Where(rec => rec.GiftId == element.GiftId);
                    // списываем
                    foreach (var giftMaterialss in giftMaterials)
                    {
                        int countOnStocks = giftMaterialss.Count * element.Count;
                        var stockMaterialss = context.StockMaterialss.Where(rec =>
                        rec.MaterialsId == giftMaterialss.MaterialsId);
                        foreach (var stockMaterials in stockMaterialss)
                        {
                            // компонентов на одном слкаде может не хватать
                            if (stockMaterials.Count >= countOnStocks)
                            {
                                stockMaterials.Count -= countOnStocks;
                                countOnStocks = 0;
                                context.SaveChanges();
                                break;
                            }
                            else
                            {
                                countOnStocks -= stockMaterials.Count;
                                stockMaterials.Count = 0;
                                context.SaveChanges();
                            }
                        }
                        if (countOnStocks > 0)
                        {
                            throw new Exception("Не достаточно компонента "+ giftMaterialss.Materials.MaterialsName + " требуется " + giftMaterialss.Count + ", нехватает " + countOnStocks);
                         }
                    }
                    element.ImplementerId = model.ImplementerId;
                    element.DateImplement = DateTime.Now;
                    element.Status = SOrderStatus.Выполняется;
                    context.SaveChanges();
                    SendEmail(element.SClient.Mail, "Оповещение по заказам", string.Format("Заказ №{0} от {1} передеан в работу", element.Id, element.DateCreate.ToShortDateString()));
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    element.Status = SOrderStatus.НедостаточноРесурсов;
                    context.SaveChanges();
                    transaction.Commit();
                    throw;
                }
            }
        }
        public void FinishOrder(SOrderBindingModel model)
        {
            SOrder element = context.SOrders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Status != SOrderStatus.Выполняется)
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }
            element.Status = SOrderStatus.Готов;
            context.SaveChanges();
            SendEmail(element.SClient.Mail, "Оповещение по заказам", string.Format("Заказ №{0} от {1} передан на оплату", element.Id, element.DateCreate.ToShortDateString()));
        }
        public void PayOrder(SOrderBindingModel model)
        {
            SOrder element = context.SOrders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Status != SOrderStatus.Готов)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }
            element.Status = SOrderStatus.Оплачен;
            context.SaveChanges();
            SendEmail(element.SClient.Mail, "Оповещение по заказам", string.Format("Заказ №{0} от {1} оплачен успешно", element.Id, element.DateCreate.ToShortDateString()));
        }
        public void PutMaterialsOnStock(StockMaterialsBindingModel model)
        {
            StockMaterials element = context.StockMaterialss.FirstOrDefault(rec =>
           rec.SStockId == model.SStockId && rec.MaterialsId == model.MaterialsId);
            if (element != null)
            {
                element.Count += model.Count;
            }
            else
            {
                context.StockMaterialss.Add(new StockMaterials
                {
                    SStockId = model.SStockId,
                    MaterialsId = model.MaterialsId,
                    Count = model.Count
                });
            }
            context.SaveChanges();
        }
        private void SendEmail(string mailAddress, string subject, string text)
        {
            MailMessage objMailMessage = new MailMessage();
            SmtpClient objSmtpClient = null;
            try
            {
                objMailMessage.From = new
               MailAddress(ConfigurationManager.AppSettings["MailLogin"]);
                objMailMessage.To.Add(new MailAddress(mailAddress));
                objMailMessage.Subject = subject;
                objMailMessage.Body = text;
                objMailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
                objMailMessage.BodyEncoding = System.Text.Encoding.UTF8;
                objSmtpClient = new SmtpClient("smtp.gmail.com", 587);
                objSmtpClient.UseDefaultCredentials = false;
                objSmtpClient.EnableSsl = true;
                objSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                objSmtpClient.Credentials = new
               NetworkCredential(ConfigurationManager.AppSettings["MailLogin"],
               ConfigurationManager.AppSettings["MailPassword"]);
                objSmtpClient.Send(objMailMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objMailMessage = null;
                objSmtpClient = null;
            }
        }
    }
}