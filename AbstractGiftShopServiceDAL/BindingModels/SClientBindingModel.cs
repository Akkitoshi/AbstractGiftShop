using System.Runtime.Serialization;

namespace AbstractGiftShopServiceDAL.BindingModels
{
    [DataContract]
    public class SClientBindingModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Mail { get; set; } 
        [DataMember]
        public string SClientFIO { get; set; }
    }
}
