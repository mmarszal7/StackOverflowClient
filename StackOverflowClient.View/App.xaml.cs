using StackOverflowClient.Common;
using StackOverflowClient.RestApiRepository;
using StackOverflowClient.SQLiteRepository;
using System.Windows;
using System;

namespace StackOverflowClient.View
{
    public partial class App : Application
    {
        // Required components
        IRestRepository restRepository;
        IDataBaseRepository dbRepository;
        NewQuestionViewModel questionViewModel; // There should be Interface, but it is not so likely that this Views will hava another ViewModels
        MainViewModel viewModel;

        // Composition Root
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            dbRepository = new DataBaseRepository();
            restRepository = new RestRepository();
            questionViewModel = new NewQuestionViewModel(dbRepository);
            viewModel = new MainViewModel(dbRepository, restRepository, CreateNewQuestionWindow);

            RunApplication();
        }

        private void RunApplication()
        {
            Application.Current.MainWindow = new MainWindow(viewModel);
            Application.Current.MainWindow.Show();
        }

        protected void CreateNewQuestionWindow()
        {
            var window = new NewQuestionWindow(questionViewModel);
            questionViewModel.OnRequestClose += (s, e) => window.Close();
            window.Show();
        }


    }
}
