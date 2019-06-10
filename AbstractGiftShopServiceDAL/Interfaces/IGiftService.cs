using AbstractGiftShopServiceDAL.Attributies;
using AbstractGiftShopServiceDAL.BindingModels;
using AbstractGiftShopServiceDAL.ViewModel;
using System.Collections.Generic;

namespace AbstractGiftShopServiceDAL.Interfaces
{
    [CustomInterface("Интерфейс для работы с подарочными наборами")]
    public interface IGiftService
    {
        [CustomMethod("Метод получения списка подарков")]
        List<GiftViewModel> GetList();
        [CustomMethod("Метод получения подарка по id")]
        GiftViewModel GetElement(int id);
        [CustomMethod("Метод добавления подарка")]
        void AddElement(GiftBindingModel model);
        [CustomMethod("Метод изменения данных по подарку")]
        void UpdElement(GiftBindingModel model);
        [CustomMethod("Метод удаления подарка")]
        void DelElement(int id);
    }
}
