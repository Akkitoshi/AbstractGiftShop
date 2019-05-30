using System.Runtime.Serialization;

namespace AbstractGiftShopServiceDAL.BindingModels
{
    [DataContract]
    public class SOrderBindingModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int SClientId { get; set; }
        [DataMember]
        public int GiftId { get; set; }
        [DataMember]
        public int Count { get; set; }
        [DataMember]
        public decimal Sum { get; set; }
}
}
