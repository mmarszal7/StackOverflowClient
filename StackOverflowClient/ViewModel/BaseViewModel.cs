using System.ComponentModel;
using System.Linq;

namespace StackOverflowClient
{
    /// <summary>
    /// Implements INotifyPropertyChanged for all ViewModels
    /// </summary>
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged()
        {
            if (PropertyChanged != null)
            {
                foreach (var property in this.GetType().GetProperties().Where(p => !p.IsSpecialName))
                    PropertyChanged(this, new PropertyChangedEventArgs(property.Name));
            }
        }
    }
}
