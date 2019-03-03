using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbstractGiftShopServiceDAL.BindingModels;
using AbstractGiftShopServiceDAL.ViewModels;

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
