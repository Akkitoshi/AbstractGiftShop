using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbstractGiftShopModel
{
    /// <summary>
    /// Исполнитель, выполняющий заказы клиентов
    /// </summary>
    public class Implementer
    {
        public int Id { get; set; }
        [Required]
        public string ImplementerFIO { get; set; }
         
        [ForeignKey("ImplementerId")]
        public virtual List<SOrder> SOrders { get; set; }
    }
}
