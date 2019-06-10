using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace AbstractGiftShopServiceDAL.ViewModel
{
    [DataContract]
    public class SClientViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DisplayName("Почтовый адрес")]
        [DataMember]
        public string Mail { get; set; }
        [DisplayName("ФИО Клиента")]
        [DataMember]
        public string SClientFIO { get; set; }
        [DataMember]
        public List<MessageInfoViewModel> Messages { get; set; }
    }
}
