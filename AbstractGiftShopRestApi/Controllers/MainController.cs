using AbstractGiftShopServiceDAL.BindingModels;
using AbstractGiftShopServiceDAL.Interfaces;
using System;
using System.Web.Http;

namespace AbstractGiftShopRestApi.Controllers
{
    public class MainController : ApiController
    {
        private readonly IMainService _service;
        public MainController(IMainService service)
        {
            _service = service;
        }
        [HttpGet]
        public IHttpActionResult GetList()
        {
            var list = _service.GetList();
            if (list == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(list);
        }
        [HttpPost]
        public void CreateOrder(SOrderBindingModel model)
        {
            _service.CreateOrder(model);
        }
        [HttpPost]
        public void TakeOrderInWork(SOrderBindingModel model)
        {
            _service.TakeOrderInWork(model);
        }
        [HttpPost]

        public void FinishOrder(SOrderBindingModel model)
        {
            _service.FinishOrder(model);
        }
        [HttpPost]
        public void PayOrder(SOrderBindingModel model)
        {
            _service.PayOrder(model);
        }
        [HttpPost]
        public void PutMaterialsOnStock(StockMaterialsBindingModel model)
        {
            _service.PutMaterialsOnStock(model);
        }
    }
}