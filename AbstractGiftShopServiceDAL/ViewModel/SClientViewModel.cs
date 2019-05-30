using System.ComponentModel;

namespace AbstractGiftShopServiceDAL.ViewModel
{
    public class SClientViewModel
    {
        public int Id { get; set; }
        [DisplayName("ФИО Клиента")]
        public string SClientFIO { get; set; }
    }
}
