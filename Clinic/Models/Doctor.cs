
namespace Clinic.Models
{
    public class Doctor : ViewModelBase
    {
        public Doctor(int id, string fullName, string post)
        {
            Id = id;
            FullName = fullName;
            Post = post;
        }
        public int Id { get; }
        string fullName = null!;
        public string FullName
        {
            get => fullName;
            set => Set(ref fullName, value);
        }
        string post = null!;
        public string Post
        {
            get => post;
            set => Set(ref post, value);
        }
    }
}
