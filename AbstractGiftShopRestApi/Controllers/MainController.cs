using AbstractGiftShopRestApi.Services;
using AbstractGiftShopServiceDAL.BindingModels;
using AbstractGiftShopServiceDAL.Interfaces;
using AbstractGiftShopServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Web.Http;
namespace AbstractGiftShopRestApi.Controllers
{
    public class MainController : ApiController
    {
        private readonly IMainService _service;
        private readonly IImplementerService _serviceImplementer;
        public MainController(IMainService service, IImplementerService
       serviceImplementer)
        {
            _service = service;
            _serviceImplementer = serviceImplementer;
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
 public void PayOrder(SOrderBindingModel model)
        {
            _service.PayOrder(model);
        }
        [HttpPost]
        public void PutMaterialsOnStock(StockMaterialsBindingModel model)
        {
            _service.PutMaterialsOnStock(model);
        }
        [HttpPost]
        public void StartWork()
        {
            List<SOrderViewModel> orders = _service.GetFreeOrders();
            foreach (var order in orders)
            {
                ImplementerViewModel impl = _serviceImplementer.GetFreeImplementer();
                if (impl == null)
                {
                    throw new Exception("Нет сотрудников");
                }
                new WorkImplementer(_service, _serviceImplementer, impl.Id, order.Id);
            }
        }
        [HttpGet]
        public IHttpActionResult GetInfo()
        {
            ReflectionService service = new ReflectionService();
            var list = service.GetInfoByAssembly();
            if (list == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(list);
        }
    }
}