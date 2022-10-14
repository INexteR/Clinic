
namespace Clinic.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public RelayCommand ToDoctors { get; } = new(_ => App.SetDoctorView(), IsDoctorsEnabled);
        public RelayCommand ToTickets { get; } = new(_ => App.SetTicketView(), IsTicketsEnabled);
        public RelayCommand BackToLogin { get; } = new(_ => App.BackToLogin());

        Patient patient = null!;
        public Patient Patient
        {
            get => patient;
            set => Set(ref patient, value);
        }
        static bool IsDoctorsEnabled(object obj)
        {
            return App.GetContent() is not DoctorView;
        }
        static bool IsTicketsEnabled(object obj)
        {
            return App.GetContent() is not TicketView;
        }
    }
}
