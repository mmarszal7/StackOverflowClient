using System.Windows;
using StackOverflowClient.ViewModel;

namespace StackOverflowClient.View
{
    public partial class MainWindow : Window
    {
        public MainWindow(MainViewModel mainViewModel)
        {
            InitializeComponent();
            this.DataContext = mainViewModel;
        }
    }
}
