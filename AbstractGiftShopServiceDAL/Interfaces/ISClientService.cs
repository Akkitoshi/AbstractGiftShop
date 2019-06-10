using System.Collections.Generic;
using AbstractGiftShopServiceDAL.Attributies;
using AbstractGiftShopServiceDAL.BindingModels;
using AbstractGiftShopServiceDAL.ViewModel;

namespace AbstractGiftShopServiceDAL.Interfaces
{
    [CustomInterface("Интерфейс для работы с клиентами")]
    public interface ISClientService
    {
        [CustomMethod("Метод получения списка клиентов")]
        List<SClientViewModel> GetList();
        [CustomMethod("Метод получения клиента по id")]
        SClientViewModel GetElement(int id);
        [CustomMethod("Метод добавления клиента")]
        void AddElement(SClientBindingModel model);
        [CustomMethod("Метод изменения данных по клиенту")]
        void UpdElement(SClientBindingModel model);
        [CustomMethod("Метод удаления клиента")]
        void DelElement(int id);
    }
}
