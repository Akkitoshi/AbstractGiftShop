using AbstractGiftShopServiceDAL.BindingModels;
using AbstractGiftShopServiceDAL.ViewModels;
using System.Collections.Generic;

namespace AbstractGiftShopServiceDAL.Interfaces
{
    public interface IMainService
    {
        List<SOrderViewModel> GetList();
        void CreateOrder(SOrderBindingModel model);
        void TakeOrderInWork(SOrderBindingModel model);
        void FinishOrder(SOrderBindingModel model);
        void PayOrder(SOrderBindingModel model);
        void PutMaterialsOnStock(StockMaterialsBindingModel model);
    }
}