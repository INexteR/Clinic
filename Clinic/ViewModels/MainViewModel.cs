
namespace Clinic.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public RelayCommand ToDoctors { get; } = new(_ => App.SetDoctorView());
        public RelayCommand ToTickets { get; } = new(_ => App.SetTicketView());
        public RelayCommand BackToLogin { get; } = new(_ => App.BackToLogin());

        Patient patient = null!;
        public Patient Patient
        {
            get => patient;
            set => Set(ref patient, value);
        }
    }
}
