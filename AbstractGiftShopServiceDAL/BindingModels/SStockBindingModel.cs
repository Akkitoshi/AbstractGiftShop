using System.Runtime.Serialization;

namespace AbstractGiftShopServiceDAL.BindingModels
{
    [DataContract]
    public class SStockBindingModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string SStockName { get; set; }
    }
}
