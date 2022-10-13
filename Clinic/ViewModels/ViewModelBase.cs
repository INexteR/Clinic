
namespace Clinic.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        protected ViewModelBase() { }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void Set<T>(ref T storage, T value, [CallerMemberName] string propertyName = null!)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
                return;
            storage = value;
            OnPropertyChanged(propertyName);
        }
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null!)
        {
            PropertyChanged?.Invoke(this, new(propertyName));
        }
    }
}
