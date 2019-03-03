using AbstractGiftShopServiceDAL.BindingModels;
using AbstractGiftShopServiceDAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
