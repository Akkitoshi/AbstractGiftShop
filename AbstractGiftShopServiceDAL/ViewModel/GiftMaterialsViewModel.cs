using System.ComponentModel;

namespace AbstractGiftShopServiceDAL.ViewModels
{
    public class GiftMaterialsViewModel
    {
        public int Id { get; set; }
        public int GiftId { get; set; }
        public int MaterialsId { get; set; }
        [DisplayName("Материал")]
        public string MaterialsName { get; set; }
        [DisplayName("Количество")]
        public int Count { get; set; }
    }
}