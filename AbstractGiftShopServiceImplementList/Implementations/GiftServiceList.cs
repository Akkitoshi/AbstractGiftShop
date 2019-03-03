using AbstractGiftShopModel;
using AbstractGiftShopServiceDAL.BindingModels;
using AbstractGiftShopServiceDAL.Interfaces;
using AbstractGiftShopServiceDAL.ViewModels;
using AbstractGiftShopServiceImplement;
using System;
using System.Collections.Generic;
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
            List<GiftViewModel> result = new List<GiftViewModel>();
            for (int i = 0; i < source.Gifts.Count; ++i)
            {
                // требуется дополнительно получить список материалов для подарка и их  количество
            List<GiftMaterialsViewModel> giftMaterialss = new List<GiftMaterialsViewModel>();
                for (int j = 0; j < source.GiftMaterialss.Count; ++j)
                {
                    if (source.GiftMaterialss[j].GiftId == source.Gifts[i].Id)
                    {
                        string materialsName = string.Empty;
                        for (int k = 0; k < source.Materialss.Count; ++k)
                        {
                            if (source.GiftMaterialss[j].MaterialsId ==
                           source.Materialss[k].Id)
                            {
                                materialsName = source.Materialss[k].MaterialsName;
                                break;
                            }
                        }
                        giftMaterialss.Add(new GiftMaterialsViewModel
                        {
                            Id = source.GiftMaterialss[j].Id,
                        GiftId = source.GiftMaterialss[j].GiftId,
                            MaterialsId = source.GiftMaterialss[j].MaterialsId,
                            MaterialsName = materialsName,
                            Count = source.GiftMaterialss[j].Count
                        });
                    }
                }
                result.Add(new GiftViewModel
                {
                    Id = source.Gifts[i].Id,
                    GiftName = source.Gifts[i].GiftName,
                    Price = source.Gifts[i].Price,
                    GiftMaterials = giftMaterialss
                });
            }
            return result;
        }
        public GiftViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Gifts.Count; ++i)
            {
                // требуется дополнительно получить список компонентов для изделия и их количество
            List<GiftMaterialsViewModel> giftMaterialss = new List<GiftMaterialsViewModel>();
                for (int j = 0; j < source.GiftMaterialss.Count; ++j)
                {
                    if (source.GiftMaterialss[j].GiftId == source.Gifts[i].Id)
                    {
                        string materialsName = string.Empty;
                        for (int k = 0; k < source.Materialss.Count; ++k)
                        {
                            if (source.GiftMaterialss[j].MaterialsId ==
                           source.Materialss[k].Id)
                            {
                                materialsName = source.Materialss[k].MaterialsName;
                                break;
                            }
                        }
                        giftMaterialss.Add(new GiftMaterialsViewModel
                        {
                            Id = source.GiftMaterialss[j].Id,
                            GiftId = source.GiftMaterialss[j].GiftId,
                            MaterialsId = source.GiftMaterialss[j].MaterialsId,
                            MaterialsName = materialsName,
                            Count = source.GiftMaterialss[j].Count
                        });
                    }
                }
                if (source.Gifts[i].Id == id)
                {
                    return new GiftViewModel
                    {
                        Id = source.Gifts[i].Id,
                        GiftName = source.Gifts[i].GiftName,
                        Price = source.Gifts[i].Price,
                        GiftMaterials = giftMaterialss
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }
 public void AddElement(GiftBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Gifts.Count; ++i)
            {
                if (source.Gifts[i].Id > maxId)
                {
                    maxId = source.Gifts[i].Id;
                }
                if (source.Gifts[i].GiftName == model.GiftName)
                {
                    throw new Exception("Уже есть подарок с таким названием");
                }
            }
            source.Gifts.Add(new Gift
            {
                Id = maxId + 1,
                GiftName = model.GiftName,
                Price = model.Price
            });
            // материалы для подарка
            int maxPCId = 0;
            for (int i = 0; i < source.GiftMaterialss.Count; ++i)
            {
                if (source.GiftMaterialss[i].Id > maxPCId)
                {
                    maxPCId = source.GiftMaterialss[i].Id;
                }
            }
            // убираем дубли по материалам
            for (int i = 0; i < model.GiftMaterialss.Count; ++i)
            {
                for (int j = 1; j < model.GiftMaterialss.Count; ++j)
                {
                    if (model.GiftMaterialss[i].MaterialsId ==
                    model.GiftMaterialss[j].MaterialsId)
                    {
                        model.GiftMaterialss[i].Count +=
                        model.GiftMaterialss[j].Count;
                        model.GiftMaterialss.RemoveAt(j--);
                    }
                }
            }
            // добавляем материалы
            for (int i = 0; i < model.GiftMaterialss.Count; ++i)
            {
                source.GiftMaterialss.Add(new GiftMaterials
                {
                    Id = ++maxPCId,
                    GiftId = maxId + 1,
                    MaterialsId = model.GiftMaterialss[i].MaterialsId,
                    Count = model.GiftMaterialss[i].Count
                });
            }
        }
        public void UpdElement(GiftBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Gifts.Count; ++i)
            {
                if (source.Gifts[i].Id == model.Id)
                {
                index = i;
                }
                if (source.Gifts[i].GiftName == model.GiftName &&
                source.Gifts[i].Id != model.Id)
                {
                    throw new Exception("Уже есть подарок с таким названием");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Gifts[index].GiftName = model.GiftName;
            source.Gifts[index].Price = model.Price;
            int maxPCId = 0;
            for (int i = 0; i < source.GiftMaterialss.Count; ++i)
            {
                if (source.GiftMaterialss[i].Id > maxPCId)
                {
                    maxPCId = source.GiftMaterialss[i].Id;
                }
            }
            // обновляем существуюущие материалы
            for (int i = 0; i < source.GiftMaterialss.Count; ++i)
            {
                if (source.GiftMaterialss[i].GiftId == model.Id)
                {
                    bool flag = true;
                    for (int j = 0; j < model.GiftMaterialss.Count; ++j)
                    {
                        // если встретили, то изменяем количество
                        if (source.GiftMaterialss[i].Id ==
                       model.GiftMaterialss[j].Id)
                        {
                            source.GiftMaterialss[i].Count =
                           model.GiftMaterialss[j].Count;
                            flag = false;
                            break;
                        }
                    }
                    // если не встретили, то удаляем
                    if (flag)
                    {
                        source.GiftMaterialss.RemoveAt(i--);
                    }
                }
            }
            // новые записи
            for (int i = 0; i < model.GiftMaterialss.Count; ++i)
            {
                if (model.GiftMaterialss[i].Id == 0)
                {
                    // ищем дубли
                    for (int j = 0; j < source.GiftMaterialss.Count; ++j)
                    {
                        if (source.GiftMaterialss[j].GiftId == model.Id &&
                        source.GiftMaterialss[j].MaterialsId ==
                       model.GiftMaterialss[i].MaterialsId)
                        {
                            source.GiftMaterialss[j].Count +=
                           model.GiftMaterialss[i].Count;
                            model.GiftMaterialss[i].Id =
                           source.GiftMaterialss[j].Id;
                            break;
                        }
                    }
                    // если не нашли дубли, то новая запись
                    if (model.GiftMaterialss[i].Id == 0)
                    {
                        source.GiftMaterialss.Add(new GiftMaterials
                        {
                            Id = ++maxPCId,
                            GiftId = model.Id,
                            MaterialsId = model.GiftMaterialss[i].MaterialsId,
                            Count = model.GiftMaterialss[i].Count
                        });
                    }
                }
            }
        }
        public void DelElement(int id)
        {
            // удаяем записи по материалам при удалении подарка
            for (int i = 0; i < source.GiftMaterialss.Count; ++i)
            {
                if (source.GiftMaterialss[i].GiftId == id)
                {
                    source.GiftMaterialss.RemoveAt(i--);
                }
            }
            for (int i = 0; i < source.Gifts.Count; ++i)
            {
                if (source.Gifts[i].Id == id)
                {
                    source.Gifts.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}