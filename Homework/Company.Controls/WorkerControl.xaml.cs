using CompanyCommunication.CompanyService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Company.Controls
{
    /// <summary>
    /// Interaction logic for WorkerControl.xaml
    /// </summary>
    public partial class WorkerControl : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Employee _employee;

        public Employee Employee
        {
            get { return _employee; }
            set
            {
                _employee = value;
                NotifyPropertyChanged();
            }
        }

        public static ObservableCollection<string> DepartmentsList { get; set; } = new ObservableCollection<string>();

        public string WorkerName { get { return tbName.Text; } }
        public string WorkerSurame { get { return tbSurname.Text; } }
        public string WorkerPatronymic { get { return tbPatronymic.Text; } }

        public WorkerControl()
        {
            InitializeComponent();
            this.DataContext = this;
            if (DepartmentsList.Count == 0)
            {
                DepartmentsList.Add("Information Security-Россия");
                DepartmentsList.Add("IT Support-Германия");
                DepartmentsList.Add("IT Development-Швеция");
                DepartmentsList.Add("Software Development-Китай");
                DepartmentsList.Add("Quality Control-США");
            }
        }

        public Employee WorkerAdder()
        {
            Employee employee = new Employee();

            employee.ID = new Random().Next(1000, 10000);
            employee.Name = tbName.Text;
            employee.Surname = tbSurname.Text;
            employee.Patronymic = tbPatronymic.Text;
            employee.Trainee = (bool)cbIsATrainee.IsChecked;
            employee.PlaceOfWork = (string)cbDepartment.SelectedItem;

            _employee = employee;

            return employee;
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
