using AbstractGiftShopServiceDAL.BindingModels;
using AbstractGiftShopServiceDAL.ViewModel;
using System.Collections.Generic;

namespace AbstractGiftShopServiceDAL.Interfaces
{
    public interface ISStockService
    {
        List<SStockViewModel> GetList();
        SStockViewModel GetElement(int id);
        void AddElement(SStockBindingModel model);
        void UpdElement(SStockBindingModel model);
        void DelElement(int id);
    }
}
