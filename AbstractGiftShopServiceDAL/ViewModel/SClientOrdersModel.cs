using System.Runtime.Serialization;

namespace AbstractGiftShopServiceDAL.ViewModel
{
    [DataContract]
    public class SClientOrdersModel
    {
        [DataMember]
        public string SClientName { get; set; }
        [DataMember]
        public string DateCreate { get; set; }
        [DataMember]
        public string GiftName { get; set; }
        [DataMember]
        public int Count { get; set; }
        [DataMember]
        public decimal Sum { get; set; }
        [DataMember]
        public string Status { get; set; }

    }
}
