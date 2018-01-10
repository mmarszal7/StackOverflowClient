using System.Windows;
using Unity;

namespace StackOverflowClient.Common
{
    public class DialogService<T> : IDialogService<T> where T : Window
    {
        private readonly IUnityContainer UnityContainer;
        private T WindowInstance;

        public DialogService(IUnityContainer unityContainer)
        {
            UnityContainer = unityContainer;
        }

        public void ShowUnique()
        {
            WindowInstance = UnityContainer.Resolve<T>();
        }

        public void Show()
        {
            if (WindowInstance != null)
                WindowInstance.Close();
            WindowInstance = UnityContainer.Resolve<T>();
            WindowInstance.Show();
        }

        public void ShowDialog()
        {
            UnityContainer.Resolve<T>().ShowDialog();
        }
    }
}
