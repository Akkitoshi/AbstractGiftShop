using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AbstractGiftShopServiceDAL.BindingModels
{
    [DataContract]
    public class GiftBindingModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string GiftName { get; set; }
        [DataMember]
        public decimal Price { get; set; }
        [DataMember]
        public List<GiftMaterialsBindingModel> GiftMaterialss { get; set; }
    }
}