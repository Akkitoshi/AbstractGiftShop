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
            var mainWindow = container.Resolve<MainWindow>();
            Application.Current.MainWindow = mainWindow;
            Application.Current.MainWindow.Show();
        }

    }
}
       /* App()
        {
            InitializeComponent();
        }
        [STAThread]
       static void Main()
        {
            App app = new App();
            var container = BuildUnityContainer();
            app.Run(container.Resolve<MainWindow>());
        }
        public static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            IUnityContainer container = new UnityContainer();
            currentContainer.RegisterType<ISClientService, ClientServiceList>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IGiftService, GiftServiceList>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMaterialsService, MaterialsServiceList>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMainService, MainServiceList>(new HierarchicalLifetimeManager());
            return currentContainer;
        }
    }
}*/