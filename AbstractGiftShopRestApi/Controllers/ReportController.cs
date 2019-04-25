using AbstractGiftShopServiceDAL.BindingModels;
using AbstractGiftShopServiceDAL.Interfaces;
using System;
using System.Web.Http;

namespace AbstractGiftShopRestApi.Controllers
{
    public class ReportController : ApiController
    {
        private readonly IReportService _service;
        public ReportController(IReportService service)
        {
            _service = service;
        }
        [HttpGet]
        public IHttpActionResult GetStocksLoad()
        {
            var list = _service.GetStocksLoad();
            if (list == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(list);
        }
        [HttpPost]
        public IHttpActionResult GetSClientOrders(ReportBindingModel model)
        {
            var list = _service.GetSClientOrders(model);
            if (list == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(list);
        }
        [HttpPost]
        public void SaveGiftPrice(ReportBindingModel model)
        {
            _service.SaveGiftPrice(model);
        }
        [HttpPost]
        public void SaveStocksLoad(ReportBindingModel model)
        {
            _service.SaveStocksLoad(model);
        }
        [HttpPost]
        public void SaveClientOrders(ReportBindingModel model)
        {
            _service.SaveClientOrders(model);
        }
    }
}