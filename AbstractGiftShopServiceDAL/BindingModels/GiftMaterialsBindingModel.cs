using System.Runtime.Serialization;

namespace AbstractGiftShopServiceDAL.BindingModels
{
    [DataContract]
    public class GiftMaterialsBindingModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int GiftId { get; set; }
        [DataMember]
        public int MaterialsId { get; set; }
        [DataMember]
        public int Count { get; set; }
    }
}