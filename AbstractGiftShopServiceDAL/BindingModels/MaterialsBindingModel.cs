using System.Runtime.Serialization;

namespace AbstractGiftShopServiceDAL.BindingModels
{
    [DataContract]
    public class MaterialsBindingModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string MaterialsName { get; set; }
    }
}
