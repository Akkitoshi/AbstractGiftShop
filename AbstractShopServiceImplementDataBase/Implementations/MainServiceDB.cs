using AbstractGiftShopModel;
using AbstractGiftShopServiceDAL.BindingModels;
using AbstractGiftShopServiceDAL.Interfaces;
using AbstractGiftShopServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;

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
                ImplementerId = rec.ImplementerId,
                ImplementerFIO = rec.Implementer.ImplementerFIO
            })
            .ToList();
            return result;
        }
       public List<SOrderViewModel> GetFreeOrders()
        {
            List<SOrderViewModel> result = context.SOrders
                .Where(x => x.Status == SOrderStatus.Принят || x.Status == SOrderStatus.НедостаточноРесурсов)
                .Select(rec => new SOrderViewModel
                {
                    Id = rec.Id
                })
                .ToList();
            return result;
        }
        public void CreateOrder(SOrderBindingModel model)
        {
            context.SOrders.Add(new SOrder
            {
                SClientId = model.SClientId,
                GiftId = model.GiftId,
                DateCreate = DateTime.Now,
                Count = model.Count,
                Sum = model.Sum,
                Status = SOrderStatus.Принят
            });
            context.SaveChanges();
        }
        public void TakeOrderInWork(SOrderBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    SOrder element = context.SOrders.FirstOrDefault(rec => rec.Id ==
                    model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    if (element.Status != SOrderStatus.Принят)
                    {
                        throw new Exception("Заказ не в статусе \"Принят\"");
                    }
                    var giftMaterialss = context.GiftMaterialss.Include(rec =>
                    rec.Materials).Where(rec => rec.GiftId == element.GiftId);
                    // списываем
                    foreach (var giftMaterials in giftMaterialss)
                    {
                        int countOnStock = giftMaterials.Count * element.Count;
                        var stockMaterialss = context.StockMaterialss.Where(rec =>
                        rec.MaterialsId == giftMaterials.MaterialsId);
                        foreach (var stockMaterials in stockMaterialss)
                        {
                            // компонентов на одном слкаде может не хватать
                            if (stockMaterials.Count >= countOnStock)
                            {
                                stockMaterials.Count -= countOnStock;
                                countOnStock = 0;
                                context.SaveChanges();
                                break;
                            }
                            else
                            {
                                countOnStock -= stockMaterials.Count;
                                stockMaterials.Count = 0;
                                context.SaveChanges();
                            }
                        }
                        if (countOnStock > 0)
                        {
                            throw new Exception("Не достаточно материала " +
                            giftMaterials.Materials.MaterialsName + " требуется " + giftMaterials.Count + ", нехватает " + countOnStock);
                        }
                    }
                    element.DateImplement = DateTime.Now;
                    element.Status = SOrderStatus.Выполняется;
                    element.ImplementerId = model.ImplementerId;
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
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
    }
}