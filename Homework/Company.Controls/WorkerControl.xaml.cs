using Company.Data;
using System;
using System.Collections.Generic;
using System.Linq;
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
    public partial class WorkerControl : UserControl
    {
        private Employee employee;

        public string WorkerName { get { return tbName.Text; } }
        public string WorkerSurame { get { return tbSurname.Text; } }
        public string WorkerPatronymic { get { return tbPatronymic.Text; } }

        public WorkerControl()
        {
            InitializeComponent();

            string[] departments = Enum.GetNames(typeof(Department));
            for(int i = 0; i < departments.Length; i++)
            {
                cbDepartment.Items.Add(departments[i]);
            }
        }

        public void WorkerGetter(Employee employee)
        {
            this.employee = employee;

            tbName.Text = employee.Name;
            tbSurname.Text = employee.Surname;
            tbPatronymic.Text = employee.Patronymic;
            cbIsATrainee.IsChecked = employee.Trainee;
            cbDepartment.SelectedItem = employee.Dep;
        }

        public Employee WorkerSetter()
        {
            employee.Name = tbName.Text;
            employee.Surname = tbSurname.Text;
            employee.Patronymic = tbPatronymic.Text;
            employee.Trainee = (bool)cbIsATrainee.IsChecked;
            employee.Dep = (string)cbDepartment.SelectedItem;

            return employee;
        }

        public Employee WorkerAdder()
        {
            employee = new Employee();

            employee.Name = tbName.Text;
            employee.Surname = tbSurname.Text;
            employee.Patronymic = tbPatronymic.Text;
            employee.Trainee = (bool)cbIsATrainee.IsChecked;
            employee.Dep = (string)cbDepartment.SelectedItem;

            return employee;
        }

        public void DepartmentAdder(string depToAdd)
        {
            cbDepartment.Items.Add(depToAdd);
        }
    }
}
