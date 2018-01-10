namespace StackOverflowClient.Common
{
    public interface IDialogService<T>
    {
        void Show();
        void ShowUnique();
        void ShowDialog();
    }
}