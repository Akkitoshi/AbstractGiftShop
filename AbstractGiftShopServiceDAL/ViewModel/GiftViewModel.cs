﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
namespace AbstractGiftShopServiceDAL.ViewModels
{
    public class GiftViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название подарка")]
        public string GiftName { get; set; }
        [DisplayName("Цена")]
        public decimal Price { get; set; }
        public List<GiftMaterialsViewModel> GiftMaterials { get; set; }
    }
}
