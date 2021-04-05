using CompanyCommunication.CompanyService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Lesson_5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static CompanyDatabase database = new CompanyDatabase();

        public ObservableCollection<Employee> EmployeesList { get; set; }

        public Employee SelectedEmployee { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            EmployeesList = database.Employees;
        }

        private void CompanyWorkersListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.AddedItems.Count != 0)
            {
                workerControl.Employee = (Employee)SelectedEmployee.Clone();
            }
        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            if (CompanyWorkersListView.SelectedItems.Count == 0) return;

            if (database.Update(workerControl.Employee) > 0)
            {
                EmployeesList[EmployeesList.IndexOf(SelectedEmployee)] = workerControl.Employee;
                MessageBox.Show("Данный о сотруднике успешно изменены", "Внесение изменений в базу данных", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (CompanyWorkersListView.SelectedItems.Count == 0) return;

            if(MessageBox.Show("Вы точно хотите удалить данного сотрудника?", "Внесение изменений в базу данных", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if(database.Remove((Employee)CompanyWorkersListView.SelectedItems[0]) > 0)
                {
                    MessageBox.Show("Сотрудник успешно удален", "Внесение изменений в базу данных", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void btnAddDepartment_Click(object sender, RoutedEventArgs e)
        {
            AddDepartment adder = new AddDepartment();
            adder.ShowDialog();
        }

        private void btnAddWorker_Click(object sender, RoutedEventArgs e)
        {
            AddWorker adder = new AddWorker();
            adder.ShowDialog();
        }
    }
}
