﻿using Company.Controls;
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

            NewEmployee.DepartmentAdder(MainWindow.database.Departments[MainWindow.database.Departments.Length - 1]);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if(NewEmployee.WorkerName == "" || NewEmployee.WorkerSurame == "" || NewEmployee.WorkerPatronymic == "")
            {
                MessageBox.Show("Вы заполнили не все данные", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            else
            {
                MainWindow.database.Employees.Add(NewEmployee.WorkerAdder());
                this.Close();
            }
        }

        public static void UpdateDepartments(string depToAdd)
        {
            
        }
    }
}