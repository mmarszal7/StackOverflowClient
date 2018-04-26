using StackOverflowClient.ViewModel;
using System.Windows;

namespace StackOverflowClient.View
{
    public partial class NewQuestionWindow : Window
    {
        public NewQuestionWindow(NewQuestionViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
