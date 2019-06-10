using AbstractGiftShopServiceDAL.Attributies;
using AbstractGiftShopServiceDAL.BindingModels;
using AbstractGiftShopServiceDAL.ViewModel;
using System.Collections.Generic;

namespace AbstractGiftShopServiceDAL.Interfaces
{
    [CustomInterface("Интерфейс для работы с материалами")]
    public interface IMaterialsService
    {
        [CustomMethod("Метод получения списка материалов")]
        List<MaterialsViewModel> GetList();
        [CustomMethod("Метод получения материала по id")]
        MaterialsViewModel GetElement(int id);
        [CustomMethod("Метод добавления материала")]
        void AddElement(MaterialsBindingModel model);
        [CustomMethod("Метод изменения данных по материалу")]
        void UpdElement(MaterialsBindingModel model);
        [CustomMethod("Метод удаления материала")]
        void DelElement(int id);
    }
}
