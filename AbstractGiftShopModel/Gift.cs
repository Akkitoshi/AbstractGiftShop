namespace AbstractGiftShopModel
{
    /// <summary>
    /// Подарок, изготавливаемый в магазине
    /// </summary>
    public class Gift
    {
        public int Id { get; set; }
        public string GiftName { get; set; }
        public decimal Price { get; set; }
    }
}
