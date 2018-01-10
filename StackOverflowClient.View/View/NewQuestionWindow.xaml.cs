using System.Windows;

namespace StackOverflowClient.View
{
    public partial class NewQuestionWindow : Window
    {
        public NewQuestionWindow(INewQuestionViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
