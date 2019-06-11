using System.Windows;
using Unity;
using AbstractGiftShopServiceDAL.Interfaces;
using Unity.Lifetime;
using AbstractGiftShopServiceImplementDataBase.Implementations;
using AbstractGiftShopServiceImplementDataBase;

namespace AbstractGiftShopWPF
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<ISClientService, SClientServiceDB>(new
           HierarchicalLifetimeManager());
            container.RegisterType<IMaterialsService, MaterialsServiceDB>(new
           HierarchicalLifetimeManager());
            container.RegisterType<IGiftService, GiftServiceDB>(new
           HierarchicalLifetimeManager());
            container.RegisterType<ISStockService, SStockServiceDB>(new
           HierarchicalLifetimeManager());
            container.RegisterType<IMainService, MainServiceDB>(new
           HierarchicalLifetimeManager());
            var mainWindow = container.Resolve<MainWindow>();
            Application.Current.MainWindow = mainWindow;
            Application.Current.MainWindow.Show();
        }

    }
}
       