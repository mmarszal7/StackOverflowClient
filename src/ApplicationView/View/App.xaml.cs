namespace StackOverflowClient.View
{
    using StackOverflowClient.Common;
    using StackOverflowClient.RestApiRepository;
    using StackOverflowClient.SQLiteRepository;
    using System.Windows;
    using System;
    using Unity;
    using Unity.Lifetime;

    public partial class App : Application
    {
        public static IUnityContainer Container;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Container = new UnityContainer();
            Container
                .RegisterType<IDataBaseRepository, DataBaseRepository>(new ContainerControlledLifetimeManager())
                .RegisterType<IRestRepository, RestRepository>(new ContainerControlledLifetimeManager());

            RunApplication();
        }

        private void RunApplication()
        {
            Application.Current.MainWindow = Container.Resolve<MainWindow>();
            Application.Current.MainWindow.Show();
        }
    }
}
