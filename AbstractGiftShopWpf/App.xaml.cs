using System.Windows;
using Unity;
using AbstractGiftShopServiceDAL.Interfaces;
using AbstractShopServiceImplementList.Implementations;
using AbstractGiftShopServiceImplementList.Implementations;
using Unity.Lifetime;
using System;

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
            container.RegisterType<IMainService, MainServiceList>();
            container.RegisterType<IMaterialsService, MaterialsServiceList>();
            container.RegisterType<IGiftService, GiftServiceList>();
            container.RegisterType<ISClientService, ClientServiceList>();
            container.RegisterType<ISStockService, StockServiceList>();
            var mainWindow = container.Resolve<MainWindow>();
            Application.Current.MainWindow = mainWindow;
            Application.Current.MainWindow.Show();
        }

    }
}