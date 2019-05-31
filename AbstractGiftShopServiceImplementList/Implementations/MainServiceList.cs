using AbstractGiftShopModel;
using AbstractGiftShopServiceDAL.BindingModels;
using AbstractGiftShopServiceDAL.Interfaces;
using AbstractGiftShopServiceDAL.ViewModel;
using AbstractGiftShopServiceImplement;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbstractGiftShopServiceImplementList.Implementations
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
                    SClientFIO = source.SClients.FirstOrDefault(recC => recC.Id == rec.SClientId)?.SClientFIO,
                    GiftName = source.Gifts.FirstOrDefault(recP => recP.Id == rec.GiftId)?.GiftName,
                })
                .ToList();
            return result;
        }
        public List<SOrderViewModel> GetFreeOrders()
        {
            List<SOrderViewModel> result = source.SOrders
                .Select(rec => new SOrderViewModel
                {
                    Id = rec.Id
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
            var GiftMaterialss = source.GiftMaterialss.Where(rec => rec.GiftId == element.GiftId);
            foreach (var GiftMaterials in GiftMaterialss)
            {
                int countOnStock = source.StockMaterialss
                    .Where(rec => rec.MaterialsId == GiftMaterials.MaterialsId)
                    .Sum(rec => rec.Count);
                if (countOnStock < GiftMaterials.Count * element.Count)
                {
                    var componentName = source.Materialss.FirstOrDefault(rec => rec.Id ==
                   GiftMaterials.MaterialsId);
                    throw new Exception("Не достаточно компонента " +
                   componentName?.MaterialsName + " требуется " + (GiftMaterials.Count * element.Count) +
                   ", в наличии " + countOnStock);
                }
            }
            // списываем
            foreach (var GiftMaterials in GiftMaterialss)
            {
                int countOnStock = GiftMaterials.Count * element.Count;
                var stockMaterialss = source.StockMaterialss.Where(rec => rec.MaterialsId
               == GiftMaterials.MaterialsId);
                foreach (var StockMaterials in stockMaterialss)
                {
                    // компонентов на одном слкаде может не хватать
                    if (StockMaterials.Count >= countOnStock)
                    {
                        StockMaterials.Count -= countOnStock;
                        break;
                    }
                    else
                    {
                        countOnStock -= StockMaterials.Count;
                        StockMaterials.Count = 0;
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