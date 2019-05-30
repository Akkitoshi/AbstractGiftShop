using AbstractGiftShopModel;
using System.Data.Entity;

namespace AbstractGiftShopServiceImplementDataBase
{
    public class AbstractGiftShopDbContext : DbContext
    {
        public AbstractGiftShopDbContext() : base("AbstractGiftShopDatabase")
        {
            //настройки конфигурации для entity
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            var ensureDLLIsCopied =
           System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
        public virtual DbSet<SClient> SClients { get; set; }
        public virtual DbSet<Materials> Materialss { get; set; }
        public virtual DbSet<SOrder> SOrders { get; set; }
        public virtual DbSet<Gift> Gifts { get; set; }
        public virtual DbSet<GiftMaterials> GiftMaterialss { get; set; }
        public virtual DbSet<SStock> Stocks { get; set; }
        public virtual DbSet<StockMaterials> StockMaterialss { get; set; }
    }
}