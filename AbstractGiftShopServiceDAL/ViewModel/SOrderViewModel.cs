using System.ComponentModel;
using System.Runtime.Serialization;

namespace AbstractGiftShopServiceDAL.ViewModel
{
    [DataContract]
    public class SOrderViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int SClientId { get; set; }
        [DataMember]
        [DisplayName("ФИО Клиента")]
        public string SClientFIO { get; set; }
        [DataMember]
        public int GiftId { get; set; }
        [DataMember]
        [DisplayName("Подарок")]
        public string GiftName { get; set; }
        [DataMember]
        [DisplayName("Количество")]
        public int Count { get; set; }
        [DataMember]
        [DisplayName("Сумма")]
        public decimal Sum { get; set; }
        [DataMember]
        [DisplayName("Статус")]
        public string Status { get; set; }
        [DataMember]
        [DisplayName("Дата создания")]
        public string DateCreate { get; set; }
        [DataMember]
        [DisplayName("Дата выполнения")]
        public string DateImplement { get; set; }
    }
}
