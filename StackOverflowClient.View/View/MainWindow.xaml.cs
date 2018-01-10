using System.Windows;

namespace StackOverflowClient.View
{
    public partial class MainWindow : Window
    {
        public MainWindow(IMainViewModel mainViewModel)
        {
            InitializeComponent();
            this.DataContext = mainViewModel;
        }
    }
}
