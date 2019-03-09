using System.ComponentModel;

namespace AbstractGiftShopServiceDAL.ViewModels
{
    public class MaterialsViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название материала")]
        public string MaterialsName { get; set; }
    }
}
