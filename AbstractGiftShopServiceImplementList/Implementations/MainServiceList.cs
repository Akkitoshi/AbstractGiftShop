using AbstractGiftShopModel;
using AbstractGiftShopServiceDAL.BindingModels;
using AbstractGiftShopServiceDAL.Interfaces;
using AbstractGiftShopServiceDAL.ViewModels;
using AbstractGiftShopServiceImplement;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbstractShopServiceImplementList.Implementations
{
    public class MainServiceList : IMainService
    {
        private DataListSingleton source;
        public MainServiceList()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<SOrderViewModel> GetList()
        {
            List<SOrderViewModel> result = source.SOrders
            .Select(rec => new SOrderViewModel
            {
                Id = rec.Id,
                SClientId = rec.SClientId,
                GiftId = rec.GiftId,
                DateCreate = rec.DateCreate.ToLongDateString(),
                DateImplement = rec.DateImplement?.ToLongDateString(),
                Status = rec.Status.ToString(),
                Count = rec.Count,
                Sum = rec.Sum,
                SClientFIO = source.SClients.FirstOrDefault(recC => recC.Id ==
     rec.SClientId)?.SClientFIO,
                GiftName = source.Gifts.FirstOrDefault(recP => recP.Id ==
    rec.GiftId)?.GiftName,
            })
            .ToList();
            return result;
        }
        public void CreateOrder(SOrderBindingModel model)
        {
            int maxId = source.SOrders.Count > 0 ? source.SOrders.Max(rec => rec.Id) : 0;
            source.SOrders.Add(new SOrder
            {
                Id = maxId + 1,
                SClientId = model.SClientId,
                GiftId = model.GiftId,
                DateCreate = DateTime.Now,
                Count = model.Count,
                Sum = model.Sum,
                Status = SOrderStatus.Принят
            });
        }
        public void TakeOrderInWork(SOrderBindingModel model)
        {
            SOrder element = source.SOrders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Status != SOrderStatus.Принят)
            {
                throw new Exception("Заказ не в статусе \"Принят\"");
            }
            // смотрим по количеству компонентов на складах
            var giftMaterialss = source.GiftMaterialss.Where(rec => rec.GiftId
           == element.GiftId);
            foreach (var giftMaterials in giftMaterialss)
            {
                int countOnStocks = source.StockMaterialss
                .Where(rec => rec.MaterialsId ==
               giftMaterials.MaterialsId)
               .Sum(rec => rec.Count);
                if (countOnStocks < giftMaterials.Count * element.Count)
                {
                    var materialsName = source.Materialss.FirstOrDefault(rec => rec.Id ==
                   giftMaterials.MaterialsId);
                    throw new Exception("Не достаточно материала " +
                   materialsName?.MaterialsName + " требуется " + (giftMaterials.Count * element.Count) +
                   ", в наличии " + countOnStocks);
                }
            }
            // списываем
            foreach (var giftMaterials in giftMaterialss)
            {
                int countOnStocks = giftMaterials.Count * element.Count;
                var stockMaterialss = source.StockMaterialss.Where(rec => rec.MaterialsId
               == giftMaterials.MaterialsId);
                foreach (var stockMaterials in stockMaterialss)
                {
                    // материалов на одном слкаде может не хватать
                    if (stockMaterials.Count >= countOnStocks)
                    {
                        stockMaterials.Count -= countOnStocks;
                        break;
                    }
                    else
                    {
                        countOnStocks -= stockMaterials.Count;
                        stockMaterials.Count = 0;
                    }
                }
            }
            element.DateImplement = DateTime.Now;
            element.Status = SOrderStatus.Выполняется;
        }

        public void FinishOrder(SOrderBindingModel model)
        {
            SOrder element = source.SOrders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Status != SOrderStatus.Выполняется)
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }
            element.Status = SOrderStatus.Готов;
        }

        public void PayOrder(SOrderBindingModel model)
        {
            SOrder element = source.SOrders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Status != SOrderStatus.Готов)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }
            element.Status = SOrderStatus.Оплачен;
        }
        public void PutMaterialsOnStock(StockMaterialsBindingModel model)
        {
            StockMaterials element = source.StockMaterialss.FirstOrDefault(rec =>
           rec.SStockId == model.SStockId && rec.MaterialsId == model.MaterialsId);
            if (element != null)
            {
                element.Count += model.Count;
            }
            else
            {
                int maxId = source.StockMaterialss.Count > 0 ?
               source.StockMaterialss.Max(rec => rec.Id) : 0;
                source.StockMaterialss.Add(new StockMaterials
                {
                    Id = ++maxId,
                    SStockId = model.SStockId,
                    MaterialsId = model.MaterialsId,
                    Count = model.Count
                });
            }
        }
    }
}