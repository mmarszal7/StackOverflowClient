using StackOverflowClient.Common;
using StackOverflowClient.RestApiRepository;
using StackOverflowClient.SQLiteRepository;
using System.Windows;
using System;
using Unity;
using Unity.Lifetime;
using StackOverflowClient.WCFserviceRepository;

namespace StackOverflowClient.View
{
    // Composition Root
    public partial class App : Application
    {
        public static IUnityContainer Container;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Container = new UnityContainer();
            Container
                .RegisterType<IDataBaseRepository, DataBaseRepository>(new ContainerControlledLifetimeManager())
                //.RegisterType<IRestRepository, RestRepository>(new ContainerControlledLifetimeManager())
                .RegisterType<IRestRepository, WCFrepository>(new ContainerControlledLifetimeManager())
                .RegisterType(typeof(IDialogService<>), typeof(DialogService<>), new ContainerControlledLifetimeManager())
                .RegisterType<IMainViewModel, MainViewModel>(new ContainerControlledLifetimeManager())
                .RegisterType<INewQuestionViewModel, NewQuestionViewModel>(new ContainerControlledLifetimeManager());

            RunApplication();
        }

        private void RunApplication()
        {
            Application.Current.MainWindow = Container.Resolve<MainWindow>();
            Application.Current.MainWindow.Show();
        }
    }
}
