using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public int Count { get; set; }
    }
}
