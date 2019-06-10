using AbstractGiftShopServiceDAL.Attributies;
using AbstractGiftShopServiceDAL.BindingModels;
using AbstractGiftShopServiceDAL.ViewModel;
using System.Collections.Generic;

namespace AbstractGiftShopServiceDAL.Interfaces
{
    [CustomInterface("Интерфейс для работы со складом")]
    public interface ISStockService
    {
        [CustomMethod("Метод получения списка складов")]
        List<SStockViewModel> GetList();
        [CustomMethod("Метод получения склада по id")]
        SStockViewModel GetElement(int id);
        [CustomMethod("Метод добавления склада")]
        void AddElement(SStockBindingModel model);
        [CustomMethod("Метод изменения данных по складу")]
        void UpdElement(SStockBindingModel model);
        [CustomMethod("Метод удаления склада")]
        void DelElement(int id);
    }
}
