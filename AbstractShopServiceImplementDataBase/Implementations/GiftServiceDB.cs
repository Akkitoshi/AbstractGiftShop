using AbstractGiftShopModel;
using AbstractGiftShopServiceDAL.BindingModels;
using AbstractGiftShopServiceDAL.Interfaces;
using AbstractGiftShopServiceDAL.ViewModels;
using AbstractGiftShopServiceImplementDataBase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbstractGiftShopServiceImplementDataBase.Implementations
{
    public class GiftServiceDB : IGiftService
    {
        private AbstractGiftShopDbContext context;
        public GiftServiceDB(AbstractGiftShopDbContext context)
        {
            this.context = context;
        }
        public List<GiftViewModel> GetList()
        {
            List<GiftViewModel> result = context.Gifts.Select(rec => new
           GiftViewModel
            {
                Id = rec.Id,
                GiftName = rec.GiftName,
                Price = rec.Price,
                GiftMaterials = context.GiftMaterialss
            .Where(recPC => recPC.GiftId == rec.Id)
           .Select(recPC => new GiftMaterialsViewModel
           {
               Id = recPC.Id,
               GiftId = recPC.GiftId,
               MaterialsId = recPC.MaterialsId,
               MaterialsName = recPC.Materials.MaterialsName,
               Count = recPC.Count
           })
           .ToList()
            })
            .ToList();
            return result;
        }
        public GiftViewModel GetElement(int id)
        {
            Gift element = context.Gifts.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new GiftViewModel
                {
                    Id = element.Id,
                    GiftName = element.GiftName,
                    Price = element.Price,
                    GiftMaterials = context.GiftMaterialss
    .Where(recPC => recPC.GiftId == element.Id)
     .Select(recPC => new GiftMaterialsViewModel
     {
         Id = recPC.Id,
         GiftId = recPC.GiftId,
         MaterialsId = recPC.MaterialsId,
         MaterialsName = recPC.Materials.MaterialsName,
         Count = recPC.Count
     })
    .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(GiftBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Gift element = context.Gifts.FirstOrDefault(rec =>
                   rec.GiftName == model.GiftName);
                    if (element != null)
                    {
                        throw new Exception("Уже есть подарок с таким названием");
                    }
                    element = new Gift
                    {
                        GiftName = model.GiftName,
                        Price = model.Price
                    };
                    context.Gifts.Add(element);
                    context.SaveChanges();
                    // убираем дубли по компонентам
                    var groupComponents = model.GiftMaterialss
                     .GroupBy(rec => rec.MaterialsId)
                    .Select(rec => new
                    {
                        MaterialsId = rec.Key,
                        Count = rec.Sum(r => r.Count)
                    });
                    // добавляем компоненты
                    foreach (var groupComponent in groupComponents)
                    {
                        context.GiftMaterialss.Add(new GiftMaterials
                        {
                            GiftId = element.Id,
                            MaterialsId = groupComponent.MaterialsId,
                            Count = groupComponent.Count
                        });
                        context.SaveChanges();
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        public void UpdElement(GiftBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Gift element = context.Gifts.FirstOrDefault(rec =>
                   rec.GiftName == model.GiftName && rec.Id != model.Id);
                    if (element != null)
                    {
                        throw new Exception("Уже есть подарок с таким названием");
                    }
                    element = context.Gifts.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    element.GiftName = model.GiftName;
                    element.Price = model.Price;
                    context.SaveChanges();
                    // обновляем существуюущие компоненты
                    var compIds = model.GiftMaterialss.Select(rec =>
                   rec.MaterialsId).Distinct();
                    var updateComponents = context.GiftMaterialss.Where(rec =>
                   rec.GiftId == model.Id && compIds.Contains(rec.MaterialsId));
                    foreach (var updateComponent in updateComponents)
                    {
                        updateComponent.Count =
                       model.GiftMaterialss.FirstOrDefault(rec => rec.Id == updateComponent.Id).Count;
                    }
                    context.SaveChanges();
                    context.GiftMaterialss.RemoveRange(context.GiftMaterialss.Where(rec =>
                    rec.GiftId == model.Id && !compIds.Contains(rec.MaterialsId)));
                    context.SaveChanges();
                    // новые записи
                    var groupComponents = model.GiftMaterialss
                    .Where(rec => rec.Id == 0)
                   .GroupBy(rec => rec.MaterialsId)
                   .Select(rec => new
                   {
                       MaterialsId = rec.Key,
                       Count = rec.Sum(r => r.Count)
                   });
                    foreach (var groupComponent in groupComponents)
                    {
                        GiftMaterials elementPC =
                       context.GiftMaterialss.FirstOrDefault(rec => rec.GiftId == model.Id &&
                       rec.MaterialsId == groupComponent.MaterialsId);
                        if (elementPC != null)
                        {
                            elementPC.Count += groupComponent.Count;
                            context.SaveChanges();
                        }
                        else
                        {
                            context.GiftMaterialss.Add(new GiftMaterials
                            {
                                GiftId = model.Id,
                                MaterialsId = groupComponent.MaterialsId,
                                Count = groupComponent.Count
                            });
                            context.SaveChanges();
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        public void DelElement(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Gift element = context.Gifts.FirstOrDefault(rec => rec.Id ==
                   id);
                    if (element != null)
                    {
                        // удаяем записи по компонентам при удалении изделия
                        context.GiftMaterialss.RemoveRange(context.GiftMaterialss.Where(rec =>
                        rec.GiftId == id));
                        context.Gifts.Remove(element);
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Элемент не найден");
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}