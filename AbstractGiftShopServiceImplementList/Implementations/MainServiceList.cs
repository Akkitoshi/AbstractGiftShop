using AbstractGiftShopModel;
using AbstractGiftShopServiceDAL.BindingModels;
using AbstractGiftShopServiceDAL.Interfaces;
using AbstractGiftShopServiceDAL.ViewModels;
using AbstractGiftShopServiceImplement;
using System;
using System.Collections.Generic;
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
            List<SOrderViewModel> result = new List<SOrderViewModel>();
            for (int i = 0; i < source.SOrders.Count; ++i)
            {
                string clientFIO = string.Empty;
                for (int j = 0; j < source.SClients.Count; ++j)
                {
                    if (source.SClients[j].Id == source.SOrders[i].SClientId)
                    {
                        clientFIO = source.SClients[j].SClientFIO;
                        break;
                    }
                }
                string giftName = string.Empty;
                for (int j = 0; j < source.Gifts.Count; ++j)
                {
                    if (source.Gifts[j].Id == source.SOrders[i].GiftId)
                    {
                        giftName = source.Gifts[j].GiftName;
                        break;
                    }
                }

                result.Add(new SOrderViewModel
                {
                    Id = source.SOrders[i].Id,
                    SClientId = source.SOrders[i].SClientId,
                    SClientFIO = clientFIO,
                    GiftId = source.SOrders[i].GiftId,
                    GiftName = giftName,
                    Count = source.SOrders[i].Count,
                    Sum = source.SOrders[i].Sum,
                    DateCreate = source.SOrders[i].DateCreate.ToLongDateString(),
                    DateImplement = source.SOrders[i].DateImplement?.ToLongDateString(),
                    Status = source.SOrders[i].Status.ToString()
                });
            }
            return result;
        }
        public void CreateOrder(SOrderBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.SOrders.Count; ++i)
            {
                if (source.SOrders[i].Id > maxId)
                {
                    maxId = source.SClients[i].Id;
                }
            }
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
            int index = -1;
            for (int i = 0; i < source.SOrders.Count; ++i)
            {
                if (source.SOrders[i].Id == model.Id)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            if (source.SOrders[index].Status != SOrderStatus.Принят)
            {
                throw new Exception("Заказ не в статусе \"Принят\"");
            }
            source.SOrders[index].DateImplement = DateTime.Now;
            source.SOrders[index].Status = SOrderStatus.Выполняется;
        }
        public void FinishOrder(SOrderBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.SOrders.Count; ++i)
            {
                if (source.SClients[i].Id == model.Id)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            if (source.SOrders[index].Status != SOrderStatus.Выполняется)
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }
            source.SOrders[index].Status = SOrderStatus.Готов;
        }
        public void PayOrder(SOrderBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.SOrders.Count; ++i)
            {
                if (source.SClients[i].Id == model.Id)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            if (source.SOrders[index].Status != SOrderStatus.Готов)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }
            source.SOrders[index].Status = SOrderStatus.Оплачен;
        }
    }
}