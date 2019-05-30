
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbstractGiftShopModel
{
    /// <summary>
    /// Материалы, требуемые для изготовления подарка
    /// </summary>
    public class Materials
    {
        public int Id { get; set; }
        [Required]
        public string MaterialsName { get; set; }
        public virtual List<GiftMaterials> GiftMaterialss { get; set; }
        public virtual List<StockMaterials> StockMaterialss { get; set; }
    }
}
