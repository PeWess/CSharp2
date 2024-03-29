﻿using Company.Controls;
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
    /// Interaction logic for AddDepartment.xaml
    /// </summary>
    public partial class AddDepartment : Window
    {
        public AddDepartment()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (tbDepToAdd.Text == "")
            {
                MessageBox.Show("Вы заполнили не все данные", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            else
            {
                Department dep = new Department(tbDepToAdd.Text, tbCountryToAdd.Text);
                if (MainWindow.database.AddDepartment(dep) > 0)
                {
                    MessageBox.Show("Департамент успешно добавлен", "Внесение изменений в базу данных", MessageBoxButton.OK, MessageBoxImage.Information);
                    WorkerControl.DepartmentsList.Add($"{tbDepToAdd.Text} - {tbCountryToAdd.Text}");
                }
                this.Close();
            }
        }
    }
}
