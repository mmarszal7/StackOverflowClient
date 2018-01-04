using StackOverflowClient.RestApiRepository;
using StackOverflowClient.SQLiteRepository;
using System.Windows;

namespace StackOverflowClient.View
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var dbRepository = new DataBaseRepository();
            var restRepository = new RestRepository();
            var viewModel = new MainViewModel(dbRepository, restRepository);
            Application.Current.MainWindow = new MainWindow(viewModel);
            Application.Current.MainWindow.Show();
        }
    }
}
