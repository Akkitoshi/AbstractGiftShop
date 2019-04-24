namespace AbstractGiftShopServiceDAL.ViewModel
{
   public class SClientOrdersModel
    {
        public string SClientName { get; set; }
        public string DateCreate { get; set; }
        public string GiftName { get; set; }
        public int Count { get; set; }
        public decimal Sum { get; set; }
        public string Status { get; set; }
    }
}
