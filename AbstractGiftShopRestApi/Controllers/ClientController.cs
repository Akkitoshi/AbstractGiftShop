using AbstractGiftShopServiceDAL.BindingModels;
using AbstractGiftShopServiceDAL.Interfaces;
using System;
using System.Web.Http;
namespace AbstractGiftShopRestApi.Controllers
{
    public class ClientController : ApiController
    {
        private readonly ISClientService _service;
        public ClientController(ISClientService service)
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
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var element = _service.GetElement(id);
            if (element == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(element);
        }
        [HttpPost]
        public void AddElement(SClientBindingModel model)
        {
            _service.AddElement(model);
        }
        [HttpPost]
        public void UpdElement(SClientBindingModel model)
        {
            _service.UpdElement(model);
        }
        [HttpPost]
        public void DelElement(SClientBindingModel model)
        {
            _service.DelElement(model.Id);
        }
    }
}