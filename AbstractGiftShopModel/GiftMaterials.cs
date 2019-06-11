using System.ComponentModel.DataAnnotations;

namespace AbstractGiftShopModel
{
    /// <summary>
    /// Сколько материала, требуется при изготовлении подарка
    /// </summary>
    public class GiftMaterials
    {
        public int Id { get; set; }

        public int GiftId { get; set; }

        public int MaterialsId { get; set; }

        public string MaterialsName { get; set; }

        public int Count { get; set; }

        public virtual Materials Materials { get; set; }
    }
}