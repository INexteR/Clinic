
namespace Clinic.Models
{
    public class Patient : ViewModelBase
    {
        public Patient(int id, string fullName, string policy)
        {
            Id = id;
            FullName = fullName;
            Policy = policy;
        }
        public int Id { get; }
        string fullName = null!;
        public string FullName
        {
            get => fullName;
            set => Set(ref fullName, value);
        }
        string policy = null!;
        public string Policy 
        {
            get => policy;
            set => Set(ref policy, value);
        }
    }
}
