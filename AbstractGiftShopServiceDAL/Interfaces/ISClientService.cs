using System.Collections.Generic;
using AbstractGiftShopServiceDAL.BindingModels;
using AbstractGiftShopServiceDAL.ViewModel;

namespace AbstractGiftShopServiceDAL.Interfaces
{
    public interface ISClientService
    {
        List<SClientViewModel> GetList();
        SClientViewModel GetElement(int id);
        void AddElement(SClientBindingModel model);
        void UpdElement(SClientBindingModel model);
        void DelElement(int id);
    }
}
