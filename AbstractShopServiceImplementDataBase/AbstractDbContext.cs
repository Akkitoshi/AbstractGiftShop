using AbstractGiftShopModel;
using System.Data.Entity;

namespace AbstractShopServiceImplementDataBase
{
    public class AbstractDbContext : DbContext
    {
 public AbstractDbContext() : base("AbstractDatabase")
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
        public virtual DbSet<SStock> SStocks { get; set; }
        public virtual DbSet<StockMaterials> StockMaterials { get; set; }
    }
}