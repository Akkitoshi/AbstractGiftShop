using System.ComponentModel;
using System.Runtime.Serialization;

namespace AbstractGiftShopServiceDAL.ViewModel
{
    [DataContract]
    public class StockMaterialsViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int SStockId { get; set; }
        [DataMember]
        public int MaterialsId { get; set; }
        [DataMember]
        [DisplayName("Название материала")]
        public string MaterialsName { get; set; }
        [DataMember]
        [DisplayName("Количество")]
        public int Count { get; set; }
    }
}

