using AbstractGiftShopModel;
using System.Collections.Generic;

namespace AbstractGiftShopServiceImplement
{
    class DataListSingleton
    {
        private static DataListSingleton instance;
        public List<SClient> SClients { get; set; }
 public List<Materials> Materialss { get; set; }
        public List<SOrder> SOrders { get; set; }
        public List<Gift> Gifts { get; set; }
        public List<GiftMaterials> GiftMaterialss { get; set; }
        private DataListSingleton()
        {
            SClients = new List<SClient>();
            Materialss = new List<Materials>();
            SOrders = new List<SOrder>();
            Gifts = new List<Gift>();
            GiftMaterialss = new List<GiftMaterials>();
        }
        public static DataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new DataListSingleton();
            }
            return instance;
        }
    }
}