using System.ComponentModel;

namespace AbstractGiftShopServiceDAL.ViewModel
{
    public class StockMaterialsViewModel
    {
        public int Id { get; set; }
        public int SStockId { get; set; }
        public int MaterialsId { get; set; }
        [DisplayName("Название материала")]
        public string MaterialsName { get; set; }
        [DisplayName("Количество")]
        public int Count { get; set; }
    }
}
