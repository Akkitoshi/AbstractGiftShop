using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace AbstractGiftShopServiceDAL.ViewModel
{
    [DataContract]
    public class MessageInfoViewModel
    {
        [DataMember]
        public string MessageId { get; set; }
        [DisplayName("ФИО клиента")]
        [DataMember]
        public string SClientFIO { get; set; }
        [DataMember]
        [DisplayName("Дата выполнения")]
        public DateTime DateDelivery { get; set; }
        [DataMember]
        [DisplayName("Заголовок")]
        public string Subject { get; set; }
        [DataMember]
        [DisplayName("Содержание")]
        public string Body { get; set; }
    }
}
