
namespace Clinic
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        const string patientsPath = @"..\..\..\Data\Patients.txt";
        const string doctorsPath = @"..\..\..\Data\Doctors.txt";
        const string ticketsPath = @"..\..\..\Data\Tickets.txt";
        const string policyExpr = @"^\d{4} \d{4} \d{4} \d{4}$";

        static MainWindow main = null!;
        static Login login = (Login)Current.MainWindow;

        static int pId;
        static int tId;

        public static void TryLogin()
        {
            string fio = login.fio.Text;
            string policy = login.policy.Text;
            Patient p;
            foreach (var patient in patients.Values)
            {
                if (string.Equals(fio, patient.FullName, StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(policy, patient.Policy, StringComparison.OrdinalIgnoreCase))
                {
                    p = patient;
                    goto Entry;
                }
            }
            Msg("Пациент не найден");
            return;
        Entry:
            foreach (var ticket in tickets)
            {
                if (ticket.Patient == p)
                {
                    ticketViewModel.Tickets.Add(ticket);
                }
            }
            Entry(p);
        }
        public static void BackToLogin()
        {
            login = new() { DataContext = loginViewModel };
            login.Show();
            main.Close();
            RewriteFile();
            doctorViewModel.SelectedDoctor = null!;
            ticketViewModel.SelectedTicket = null!;
            ticketViewModel.Tickets.Clear();
        }
        public static void DeleteTicket()
        {
            if (!rewriteTickets) rewriteTickets = true;
            tickets.Remove(ticketViewModel.SelectedTicket);
            ticketViewModel.Tickets.Remove(ticketViewModel.SelectedTicket);
        }
        public static async void Register()
        {
            string fio = login.fio.Text;
            var policy = login.policy.Text;
            if (string.IsNullOrWhiteSpace(fio))
            {
                Msg("Введите ФИО");
                return;
            }
            else if (!Regex.IsMatch(policy, policyExpr))
            {
                Msg("Полис должен состоять из 4 комбинаций 4 цифр, разделенных пробелами");
                return;
            }
            var p = new Patient(++pId, fio, policy);
            patients.Add(p.Id, p); 
            policy = policy.Replace(' ', '▓');
            await File.AppendAllTextAsync(patientsPath, string.Join(' ', pId, fio, policy) + '\n');
            Msg("Регистрация прошла успешно", "Сообщение");
            Entry(p);
           
        }              
        public static async void AddTicket()
        {
            var patient = mainViewModel.Patient;
            var doctor = doctorViewModel.SelectedDoctor;
            var date = doctorViewModel.DateTime!.Value;
            var ticket = new Ticket(++tId, patient, doctor, date);
            tickets.Add(ticket);
            ticketViewModel.Tickets.Add(ticket);
            await File.AppendAllTextAsync(ticketsPath, string.Join(' ', tId, patient.Id, doctor.Id, date) + '\n');
            doctorViewModel.SelectedDoctor = null!;
            doctorViewModel.DateTime = DateTime.Today;
            Msg("Талон успешно добавлен", "Сообщение", true);
        }

        static bool rewriteTickets;
        static void Entry(Patient patient)
        {
            mainViewModel.Patient = patient;
            main = new() { DataContext = mainViewModel };
            SetDoctorView();
            main.Show();
            login.Close();
        }

        static readonly Dictionary<int, Doctor> doctors = new();
        static readonly Dictionary<int, Patient> patients = new();
        static readonly List<Ticket> tickets = new();

        static bool flag;
        private void Application_Activated(object sender, EventArgs e)
        {
            if (flag) return;
            flag = true;
            login.DataContext = loginViewModel;
            Parallel.Invoke(new[] { ReadPatients, ReadDoctors });
            ReadTickets();
        }
        private void Application_Deactivated(object sender, EventArgs e)
        {
            RewriteFile();
        }
        static void RewriteFile()
        {
            if (rewriteTickets)
            {
                var ie = tickets.Select(t => string.Join(' ', t.Id, t.Patient.Id, t.Doctor.Id, t.DateTime));
                File.WriteAllLines(ticketsPath, ie);
                rewriteTickets = false;
            }
        }

        static void ReadPatients()
        {
            var lines = File.ReadAllLines(patientsPath);         
            foreach (var line in lines)
            {
                var props = line.Split();
                int id = int.Parse(props[0]);
                string fio = props[1].Replace('▓', ' ');
                string policy = props[2].Replace('▓', ' ');
                patients.Add(id, new(id, fio, policy));
            }
            pId = int.Parse(string.Concat(lines[^1].TakeWhile(ch => ch != ' ')));
        }
        static void ReadDoctors()
        {
            var lines = File.ReadAllLines(doctorsPath);
            foreach (var line in lines)
            {
                var props = line.Split();
                int id = int.Parse(props[0]);
                string fio = props[1].Replace('▓', ' ');
                doctors.Add(id, new(id, fio, props[2]));
                doctorViewModel.Doctors.Add(doctors[id]);
            }
        }
        static void ReadTickets()
        {
            var lines = File.ReadAllLines(ticketsPath);
            foreach (var line in lines)
            {
                var props = line.Split();
                int id = int.Parse(props[0]);
                int patientId = int.Parse(props[1]);
                Patient p = patients[patientId];
                int doctorId = int.Parse(props[2]);
                Doctor d = doctors[doctorId];
                DateTime dateTime = DateTime.Parse(props[3]);
                tickets.Add(new(id, p, d, dateTime));
            }
            tId = int.Parse(string.Concat(lines[^1].TakeWhile(ch => ch != ' ')));
        }

        static readonly LoginViewModel loginViewModel = new();
        static readonly MainViewModel mainViewModel = new(); 
        static readonly DoctorViewModel doctorViewModel= new();
        static readonly TicketViewModel ticketViewModel = new();

        static readonly DoctorView doctorView = new() { DataContext = doctorViewModel };
        static readonly TicketView ticketView = new() { DataContext = ticketViewModel };

        public static void SetDoctorView()
        {
            main.view.Content = doctorView;
        }
        public static void SetTicketView()
        {
            main.view.Content = ticketView;
        }

        static void Msg(string text, string? title = null, bool showIcn = false)
        {
            if (showIcn)
                MessageBox.Show(text, title, default, MessageBoxImage.Information);
            else MessageBox.Show(text, title);
        }
    }
}
