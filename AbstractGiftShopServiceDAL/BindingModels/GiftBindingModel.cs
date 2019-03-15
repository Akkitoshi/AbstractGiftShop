using System.Collections.Generic;

namespace AbstractGiftShopServiceDAL.BindingModels
{
    public class GiftBindingModel
    {
        public int Id { get; set; }
        public string GiftName { get; set; }
        public decimal Price { get; set; }
        public List<GiftMaterialsBindingModel> GiftMaterialss { get; set; }
    }
}