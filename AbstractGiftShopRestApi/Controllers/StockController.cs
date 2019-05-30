﻿using AbstractGiftShopServiceDAL.BindingModels;
using AbstractGiftShopServiceDAL.Interfaces;
using System;
using System.Web.Http;

namespace AbstractGiftShopRestApi.Controllers
{
    public class StockController : ApiController
    {
        private readonly ISStockService _service;
        public StockController(ISStockService service)
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
        public void AddElement(SStockBindingModel model)
        {
            _service.AddElement(model);
        }
        [HttpPost]
        public void UpdElement(SStockBindingModel model)
        {
            _service.UpdElement(model);
        }
        [HttpPost]
        public void DelElement(SStockBindingModel model)
        {
            _service.DelElement(model.Id);
        }
    }
}