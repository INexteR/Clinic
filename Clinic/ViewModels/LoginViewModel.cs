
namespace Clinic.ViewModels
{
    public class LoginViewModel
    {
        public RelayCommand Enter { get; } = new(_ => App.TryLogin());
        public RelayCommand Register { get; } = new(_ => App.Register());
    }
}
