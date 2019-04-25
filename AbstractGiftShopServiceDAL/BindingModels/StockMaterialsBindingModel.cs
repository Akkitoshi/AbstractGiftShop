using System.Runtime.Serialization;

namespace AbstractGiftShopServiceDAL.BindingModels
{
    [DataContract]
    public class StockMaterialsBindingModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int SStockId { get; set; }
        [DataMember]
        public int MaterialsId { get; set; }
        [DataMember]
        public int Count { get; set; }
    }
}
