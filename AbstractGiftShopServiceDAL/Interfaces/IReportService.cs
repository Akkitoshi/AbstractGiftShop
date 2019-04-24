using AbstractGiftShopServiceDAL.BindingModels;
using AbstractGiftShopServiceDAL.ViewModel;
using System.Collections.Generic;

namespace AbstractGiftShopServiceDAL.Interfaces
{
    public interface IReportService
    {
        void SaveGiftPrice(ReportBindingModel model);
        List<StocksLoadViewModel> GetStocksLoad();
        void SaveStocksLoad(ReportBindingModel model);
        List<SClientOrdersModel> GetSClientOrders(ReportBindingModel model);
        void SaveClientOrders(ReportBindingModel model);
    }
}
