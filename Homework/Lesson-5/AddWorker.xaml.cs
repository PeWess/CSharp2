using Company.Controls;
using CompanyCommunication.CompanyService;
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
using System.Windows.Shapes;

namespace Lesson_5
{
    /// <summary>
    /// Interaction logic for AddWorker.xaml
    /// </summary>
    public partial class AddWorker : Window
    {
        public AddWorker()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if(NewEmployee.Name == "" || NewEmployee.WorkerSurame == "" || NewEmployee.WorkerPatronymic == "")
            {
                MessageBox.Show("Вы заполнили не все данные", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            else
            {
                Employee emp = NewEmployee.WorkerAdder();
                while(MainWindow.database.Identifiers.Contains(emp.ID) == true)
                {
                    emp.ID = new Random().Next(1000, 10000);
                }
                if(MainWindow.database.AddEmployee(emp) > 0)
                {
                    MessageBox.Show("Сотрудник успешно добавлен", "Внесение изменений в базу данных", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                this.Close();
            }
        }
    }
}
