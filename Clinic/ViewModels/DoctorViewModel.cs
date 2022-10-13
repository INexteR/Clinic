
namespace Clinic.ViewModels
{
    public class DoctorViewModel : ViewModelBase
    {
        public DoctorViewModel()
        {        
            AddTicket = new(obj => App.AddTicket(), IsAddTicketEnabled);
        }
        public ObservableCollection<Doctor> Doctors { get; } = new();

        Doctor selectedDoctor = null!;
        public Doctor SelectedDoctor
        {
            get => selectedDoctor;
            set => Set(ref selectedDoctor, value);
        }
        DateTime? dateTime = System.DateTime.Today;
        public DateTime? DateTime
        {
            get => dateTime;
            set => Set(ref dateTime, value);
        }
        public RelayCommand AddTicket { get; }
        public bool IsAddTicketEnabled(object obj)
        {
            return selectedDoctor != null && dateTime != default;
        }
    }
}
