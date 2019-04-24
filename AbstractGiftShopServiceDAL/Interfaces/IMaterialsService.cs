using AbstractGiftShopServiceDAL.BindingModels;
using AbstractGiftShopServiceDAL.ViewModel;
using System.Collections.Generic;

namespace AbstractGiftShopServiceDAL.Interfaces
{
    public interface IMaterialsService
    {
        List<MaterialsViewModel> GetList();
        MaterialsViewModel GetElement(int id);
        void AddElement(MaterialsBindingModel model);
        void UpdElement(MaterialsBindingModel model);
        void DelElement(int id);
    }
}
