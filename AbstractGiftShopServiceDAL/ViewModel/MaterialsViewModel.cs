using System.Collections.Generic;
using System.ComponentModel;

namespace AbstractGiftShopServiceDAL.ViewModel
{
    public class MaterialsViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название материала")]
        public string MaterialsName { get; set; }
    }
}
