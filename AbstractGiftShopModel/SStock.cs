using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbstractGiftShopModel
{
    /// <summary>
    /// Хранилиище материалов в магазине
    /// </summary>
    public class SStock
    {
        public int Id { get; set; }
        [Required]
        public string SStockName { get; set; }
        public virtual List<StockMaterials> StockMaterialss { get; set; }
    }
}
