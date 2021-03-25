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

namespace Lesson_5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static CompanyDatabase database = new CompanyDatabase();

        public MainWindow()
        {
            InitializeComponent();
            UpdateBindings(); 
        }

        private void CompanyWorkersListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.AddedItems.Count != 0)
            {
                workerControl.WorkerGetter(e.AddedItems[0] as Employee);
            }
        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            if (CompanyWorkersListView.SelectedItems.Count == 0) return;

            if (CompanyWorkersListView.SelectedItems.Count != 0)
            {
                workerControl.WorkerSetter();
            }
            UpdateBindings();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (CompanyWorkersListView.SelectedItems.Count == 0) return;

            if(MessageBox.Show("Вы точно хотите удалить данного сотрудника?", "Внесение изменений в базу данных", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                database.Employees.Remove((Employee)CompanyWorkersListView.SelectedItems[0]);
            }
            UpdateBindings();
        }

        private void UpdateBindings()
        {
            CompanyWorkersListView.ItemsSource = null;
            CompanyWorkersListView.ItemsSource = database.Employees;
        }

        private void btnAddDepartment_Click(object sender, RoutedEventArgs e)
        {
            AddDepartment adder = new AddDepartment();
            adder.ShowDialog();
            workerControl.DepartmentAdder(database.Departments[database.Departments.Length - 1]);
            UpdateBindings();
        }

        private void btnAddWorker_Click(object sender, RoutedEventArgs e)
        {
            AddWorker adder = new AddWorker();
            adder.ShowDialog();
            UpdateBindings();
        }
    }
}
