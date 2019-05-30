using System.Collections.Generic;
using System.ComponentModel;

namespace AbstractGiftShopServiceDAL.ViewModel
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
