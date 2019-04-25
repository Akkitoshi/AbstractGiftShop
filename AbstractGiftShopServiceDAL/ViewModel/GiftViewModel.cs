using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace AbstractGiftShopServiceDAL.ViewModel
{
    [DataContract]
    public class GiftViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DisplayName("Название подарка")]
        [DataMember]
        public string GiftName { get; set; }
        [DataMember]
        [DisplayName("Цена")]
        public decimal Price { get; set; }
        [DataMember]
        public List<GiftMaterialsViewModel> GiftMaterials { get; set; }
    }
}
