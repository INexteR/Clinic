
namespace Clinic.ViewModels
{
    public class TicketViewModel : ViewModelBase
    {
        public TicketViewModel()
        {
            DeleteTicket = new(obj => App.DeleteTicket(), IsDeleteEnabled);
        }
        public ObservableCollection<Ticket> Tickets { get; } = new();
        Ticket selectedTicket = null!;
        public Ticket SelectedTicket
        {
            get => selectedTicket;
            set => Set(ref selectedTicket, value);
        }
        public RelayCommand DeleteTicket { get; }
        bool IsDeleteEnabled(object obj)
        {
            return selectedTicket != null;
        }
    }
}
