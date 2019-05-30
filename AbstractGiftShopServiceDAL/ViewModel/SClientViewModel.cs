using System.ComponentModel;
using System.Runtime.Serialization;

namespace AbstractGiftShopServiceDAL.ViewModel
{
    [DataContract]
    public class SClientViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DisplayName("ФИО Клиента")]
        [DataMember]
        public string SClientFIO { get; set; }
    }
}
