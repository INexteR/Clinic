
namespace Clinic.Models
{
    public class Ticket : ViewModelBase
    {
        public Ticket(int id, Patient patient, Doctor doctor, DateTime dateTime)
        {
            Id = id;
            Patient = patient;
            Doctor = doctor;
            DateTime = dateTime;
        }
        public int Id { get; }
        Patient patient = null!;
        public Patient Patient
        {
            get => patient;
            set => Set(ref patient, value);
        }
        Doctor doctor = null!;
        public Doctor Doctor
        {
            get => doctor;
            set => Set(ref doctor, value);
        }
        DateTime dateTime;
        public DateTime DateTime
        {
            get => dateTime;
            set => Set(ref dateTime, value);
        }
    }
}
