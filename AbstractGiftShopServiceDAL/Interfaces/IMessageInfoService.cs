using AbstractGiftShopServiceDAL.Attributies;
using AbstractGiftShopServiceDAL.BindingModels;
using AbstractGiftShopServiceDAL.ViewModel;
using System.Collections.Generic;

namespace AbstractGiftShopServiceDAL.Interfaces
{
    [CustomInterface("Интерфейс для работы с почтой")]
    public interface IMessageInfoService
    {
        [CustomMethod("Метод получения списка сообщений")]
        List<MessageInfoViewModel> GetList();
        [CustomMethod("Метод получения собщений по id")]
        MessageInfoViewModel GetElement(int id);
        [CustomMethod("Метод добавления сообщений")]
        void AddElement(MessageInfoBindingModel model);
    }
}
