using System.ComponentModel;

namespace AbstractGiftShopServiceDAL.ViewModel
{
    public class GiftMaterialsViewModel
    {
        public int Id { get; set; }
        public int GiftId { get; set; }
        public int MaterialsId { get; set; }
        public string MaterialsName { get; set; }
        public int Count { get; set; }
    }
}