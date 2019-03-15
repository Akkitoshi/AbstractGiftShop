using System.ComponentModel;

namespace AbstractGiftShopServiceDAL.ViewModels
{
    public class SClientViewModel
    {
        public int Id { get; set; }
        [DisplayName("ФИО Клиента")]
        public string SClientFIO { get; set; }
    }
}
