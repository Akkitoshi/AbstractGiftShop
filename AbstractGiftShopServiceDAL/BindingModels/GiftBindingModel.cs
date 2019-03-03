using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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