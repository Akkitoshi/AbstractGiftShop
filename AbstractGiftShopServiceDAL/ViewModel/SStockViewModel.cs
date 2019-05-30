using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace AbstractGiftShopServiceDAL.ViewModel
{
    [DataContract]
    public class SStockViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DisplayName("Название склада")]
        [DataMember]
        public string SStockName { get; set; }
        [DataMember]
        public List<StockMaterialsViewModel> StockMaterialss { get; set; }
    }
}

