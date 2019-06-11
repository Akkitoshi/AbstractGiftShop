using AbstractGiftShopServiceDAL.Interfaces;
using AbstractGiftShopServiceImplementDataBase;
using AbstractGiftShopServiceImplementDataBase.Implementations;
using AbstractGiftShopView;
using AbstractGiftShopServiceImplementDataBase.Implementations;
using System;
using System.Data.Entity;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;

namespace AbstractShopView
{
    static class Program
    {/// <summary>
     /// Главная точка входа для приложения.
     /// </summary>
        [STAThread]
        static void Main()
        {
            var container = BuildUnityContainer();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<FormMain>());
        }
        public static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<DbContext, AbstractGiftShopDbContext>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<ISClientService, SClientServiceDB>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMaterialsService, MaterialsServiceDB>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IGiftService, GiftServiceDB>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<ISStockService, SStockServiceDB>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMainService, MainServiceDB>(new
           HierarchicalLifetimeManager());
            return currentContainer;
        }
    }
}