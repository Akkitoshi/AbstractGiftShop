using System.ComponentModel;
using System.Runtime.Serialization;

namespace AbstractGiftShopServiceDAL.ViewModel
{
    [DataContract]
    public class MaterialsViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DisplayName("Название материала")]
        [DataMember]
        public string MaterialsName { get; set; }
    }
}
