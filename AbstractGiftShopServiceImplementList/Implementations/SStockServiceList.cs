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
    public class StockServiceList : ISStockService
    {
        private DataListSingleton source;
        public StockServiceList()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<SStockViewModel> GetList()
        {
            List<SStockViewModel> result = source.Stocks
            .Select(rec => new SStockViewModel
            {
                Id = rec.Id,
                SStockName = rec.SStockName,
                StockMaterialss = source.StockMaterialss
                .Where(recPC => recPC.SStockId == rec.Id)
           .Select(recPC => new StockMaterialsViewModel
           {
               Id = recPC.Id,
               SStockId = recPC.SStockId,
               MaterialsId = recPC.MaterialsId,
               MaterialsName = source.Materialss
            .FirstOrDefault(recC => recC.Id ==
           recPC.MaterialsId)?.MaterialsName,
               Count = recPC.Count
           })
           .ToList()
            })
            .ToList();
            return result;
        }
        public SStockViewModel GetElement(int id)
        {
            SStock element = source.Stocks.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new SStockViewModel
                {
                    Id = element.Id,
                    SStockName = element.SStockName,
                    StockMaterialss = source.StockMaterialss
                .Where(recPC => recPC.SStockId == element.Id)
               .Select(recPC => new StockMaterialsViewModel
               {
                   Id = recPC.Id,
                   SStockId = recPC.SStockId,
                   MaterialsId = recPC.MaterialsId,
                   MaterialsName = source.Materialss
                .FirstOrDefault(recC => recC.Id ==
               recPC.MaterialsId)?.MaterialsName,
                   Count = recPC.Count
               })
               .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(SStockBindingModel model)
        {
            SStock element = source.Stocks.FirstOrDefault(rec => rec.SStockName ==
           model.SStockName);
            if (element != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            int maxId = source.Stocks.Count > 0 ? source.Stocks.Max(rec => rec.Id) : 0;
            source.Stocks.Add(new SStock
            {
                Id = maxId + 1,
                SStockName = model.SStockName
            });
        }
        public void UpdElement(SStockBindingModel model)
        {
            SStock element = source.Stocks.FirstOrDefault(rec =>
            rec.SStockName == model.SStockName && rec.Id !=
           model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            element = source.Stocks.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.SStockName = model.SStockName;
        }
        public void DelElement(int id)
        {
            SStock element = source.Stocks.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                // при удалении удаляем все записи о компонентах на удаляемом складе
                source.StockMaterialss.RemoveAll(rec => rec.SStockId == id);
                source.Stocks.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}