namespace AbstractGiftShopServiceDAL.BindingModels
{
    public class GiftMaterialsBindingModel
    {
        public int Id { get; set; }
        public int GiftId { get; set; }
        public int MaterialsId { get; set; }
        public int Count { get; set; }
    }
}