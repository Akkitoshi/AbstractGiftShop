using AbstractGiftShopModel;
using AbstractGiftShopServiceDAL.BindingModels;
using AbstractGiftShopServiceDAL.Interfaces;
using AbstractGiftShopServiceDAL.ViewModel;
using AbstractGiftShopServiceImplement;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbstractShopServiceImplementList.Implementations
{
    public class GiftServiceList : IGiftService
    {
        private DataListSingleton source;
        public GiftServiceList()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<GiftViewModel> GetList()
        {
            List<GiftViewModel> result = source.Gifts
            .Select(rec => new GiftViewModel
            {
                Id = rec.Id,
                GiftName = rec.GiftName,
                Price = rec.Price,
                GiftMaterials = source.GiftMaterialss.Where(recPC => recPC.GiftId == rec.Id)
                .Select(recPC => new GiftMaterialsViewModel
                {
                    Id = recPC.Id,
                    GiftId = recPC.GiftId,
                    MaterialsId = recPC.MaterialsId,
                    MaterialsName = source.Materialss.FirstOrDefault(recC => recC.Id == recPC.MaterialsId)?.MaterialsName,
                    Count = recPC.Count
                })
                .ToList()
            })
            .ToList();
            return result;
        }
        public GiftViewModel GetElement(int id)
        {
            Gift element = source.Gifts.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new GiftViewModel
                {
                    Id = element.Id,
                    GiftName = element.GiftName,
                    Price = element.Price,
                    GiftMaterials = source.GiftMaterialss
                .Where(recPC => recPC.GiftId == element.Id)
                .Select(recPC => new GiftMaterialsViewModel
                {
                    Id = recPC.Id,
                    GiftId = recPC.GiftId,
                    MaterialsId = recPC.MaterialsId,
                    MaterialsName = source.Materialss.FirstOrDefault(recC =>
 recC.Id == recPC.MaterialsId)?.MaterialsName,
                    Count = recPC.Count
                })
                .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(GiftBindingModel model)
        {
            Gift element = source.Gifts.FirstOrDefault(rec => rec.GiftName ==
           model.GiftName);
            if (element != null)
            {
                throw new Exception("Уже есть подарок с таким названием");
            }
            int maxId = source.Gifts.Count > 0 ? source.Gifts.Max(rec => rec.Id) :
           0;
            source.Gifts.Add(new Gift
            {
                Id = maxId + 1,
                GiftName = model.GiftName,
                Price = model.Price
            });
            // материалы для подарка
            int maxPCId = source.GiftMaterialss.Count > 0 ?
           source.GiftMaterialss.Max(rec => rec.Id) : 0;
            // убираем дубли по материалами
            var groupMaterialss = model.GiftMaterialss
            .GroupBy(rec => rec.MaterialsId)
           .Select(rec => new
           {
               MaterialsId = rec.Key,
               Count = rec.Sum(r => r.Count)
           });
            // добавляем компоненты
            foreach (var groupMaterials in groupMaterialss)
            {
                source.GiftMaterialss.Add(new GiftMaterials
                {
                    Id = ++maxPCId,
                    GiftId = maxId + 1,
                    MaterialsId = groupMaterials.MaterialsId,
                    Count = groupMaterials.Count
                });
            }
        }
        public void UpdElement(GiftBindingModel model)
        {
            Gift element = source.Gifts.FirstOrDefault(rec => rec.GiftName ==
           model.GiftName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть подарок с таким названием");
            }
            element = source.Gifts.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.GiftName = model.GiftName;
            element.Price = model.Price;
            int maxPCId = source.GiftMaterialss.Count > 0 ?
           source.GiftMaterialss.Max(rec => rec.Id) : 0;
            // обновляем существуюущие материалы
            var matIds = model.GiftMaterialss.Select(rec =>
           rec.MaterialsId).Distinct();
            var updateMaterialss = source.GiftMaterialss.Where(rec => rec.GiftId ==
           model.Id && matIds.Contains(rec.MaterialsId));
            foreach (var updateMaterials in updateMaterialss)
            {
                updateMaterials.Count = model.GiftMaterialss.FirstOrDefault(rec =>
               rec.Id == updateMaterials.Id).Count;
            }
            source.GiftMaterialss.RemoveAll(rec => rec.GiftId == model.Id &&
           !matIds.Contains(rec.MaterialsId));
            // новые записи
            var groupMaterialss = model.GiftMaterialss
            .Where(rec => rec.Id == 0)
           .GroupBy(rec => rec.MaterialsId)
           .Select(rec => new
           {
               MaterialsId = rec.Key,
               Count = rec.Sum(r => r.Count)
           });
            foreach (var groupMaterials in groupMaterialss)
            {
                GiftMaterials elementPC = source.GiftMaterialss.FirstOrDefault(rec
               => rec.GiftId == model.Id && rec.MaterialsId == groupMaterials.MaterialsId);
                if (elementPC != null)
                {
                    elementPC.Count += groupMaterials.Count;
                }
                else
                {
                    source.GiftMaterialss.Add(new GiftMaterials
                    {
                        Id = ++maxPCId,
                        GiftId = model.Id,
                        MaterialsId = groupMaterials.MaterialsId,
                        Count = groupMaterials.Count
                    });
                }
            }
        }
        public void DelElement(int id)
        {
            Gift element = source.Gifts.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                // удаяем записи по материалам при удалении подарка
                source.GiftMaterialss.RemoveAll(rec => rec.GiftId == id);
                source.Gifts.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
