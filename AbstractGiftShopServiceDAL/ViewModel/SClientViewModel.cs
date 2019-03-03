using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractGiftShopServiceDAL.ViewModels
{
    public class SClientViewModel
    {
        public int Id { get; set; }
        [DisplayName("ФИО Клиента")]
        public string SClientFIO { get; set; }
    }
}
