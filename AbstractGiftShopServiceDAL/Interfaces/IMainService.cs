using AbstractGiftShopServiceDAL.Attributies;
using AbstractGiftShopServiceDAL.BindingModels;
using AbstractGiftShopServiceDAL.ViewModel;
using System.Collections.Generic;

namespace AbstractGiftShopServiceDAL.Interfaces
{
    [CustomInterface("Интерфейс для работы с заказами")]
    public interface IMainService
    {
        [CustomMethod("Метод получения списка заказов")]
        List<SOrderViewModel> GetList();
        [CustomMethod("Метод получения свободных заказов")]
        List<SOrderViewModel> GetFreeOrders();
        [CustomMethod("Метод создания заказа")]
        void CreateOrder(SOrderBindingModel model);
        [CustomMethod("Метод выполнения заказа")]
        void TakeOrderInWork(SOrderBindingModel model);
        [CustomMethod("Метод завершения выполнения заказа")]
        void FinishOrder(SOrderBindingModel model);
        [CustomMethod("Метод оплаты заказа")]
        void PayOrder(SOrderBindingModel model);
        [CustomMethod("Метод пополнения склада")]
        void PutMaterialsOnStock(StockMaterialsBindingModel model);
    }
}