using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbstractGiftShopModel
{
    /// <summary>
    /// Подарок, изготавливаемый в магазине
    /// </summary>
    public class Gift
    {
        public int Id { get; set; }
        [Required]
        public string GiftName { get; set; }
        [Required]
        public decimal Price { get; set; }
        public virtual List<SOrder> SOrders { get; set; }
    }
}