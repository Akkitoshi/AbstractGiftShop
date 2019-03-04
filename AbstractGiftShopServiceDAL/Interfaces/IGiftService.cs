using AbstractGiftShopServiceDAL.BindingModels;
using AbstractGiftShopServiceDAL.ViewModels;
using System.Collections.Generic;

namespace AbstractGiftShopServiceDAL.Interfaces
{
    public interface IGiftService
    {
        List<GiftViewModel> GetList();
        GiftViewModel GetElement(int id);
        void AddElement(GiftBindingModel model);
        void UpdElement(GiftBindingModel model);
        void DelElement(int id);
    }
}
