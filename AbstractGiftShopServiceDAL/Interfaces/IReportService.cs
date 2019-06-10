using AbstractGiftShopServiceDAL.Attributies;
using AbstractGiftShopServiceDAL.BindingModels;
using AbstractGiftShopServiceDAL.ViewModel;
using System.Collections.Generic;

namespace AbstractGiftShopServiceDAL.Interfaces
{
    [CustomInterface("Интерфейс для работы с отчетами")]
    public interface IReportService
    {
        [CustomMethod("Метод сохранения отчета по ценам подарков")]
        void SaveGiftPrice(ReportBindingModel model);
        [CustomMethod("Метод получения отчета по загруженности складов")]
        List<StocksLoadViewModel> GetStocksLoad();
        [CustomMethod("Метод сохранения отчета по загруженности складов")]
        void SaveStocksLoad(ReportBindingModel model);
        [CustomMethod("Метод получения отчета по заказам клиентов")]
        List<SClientOrdersModel> GetSClientOrders(ReportBindingModel model);
        [CustomMethod("Метод сохранения отчета по заказам клиентов")]
        void SaveClientOrders(ReportBindingModel model);
    }
}
