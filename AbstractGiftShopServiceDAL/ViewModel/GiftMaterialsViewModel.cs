using System.ComponentModel;
using System.Runtime.Serialization;

namespace AbstractGiftShopServiceDAL.ViewModel
{
    [DataContract]
    public class GiftMaterialsViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int GiftId { get; set; }
        [DataMember]
        public int MaterialsId { get; set; }
        [DataMember]
        public string MaterialsName { get; set; }
        [DataMember]
        public int Count { get; set; }
    }
}