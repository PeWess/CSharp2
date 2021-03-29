using Company.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_5
{
    public class CompanyDatabase
    {
        Random rnd = new Random();
        string filename = AppDomain.CurrentDomain.BaseDirectory + "Names.txt";

        public List<string> departments = new List<string>{
        "Information Security",
        "IT Support",
        "IT Development",
        "Software Development",
        "Quality Control"};

        public List<string> countries = new List<string>{
        "Россия",
        "Германия",
        "Швеция",
        "Китай",
        "США"};

        public ObservableCollection<Employee> Employees{ get; set; }
        public ObservableCollection<Department> Departments { get; set; }

        public CompanyDatabase()
        {
            Employees = new ObservableCollection<Employee>();
            Departments = new ObservableCollection<Department>();
            GenerateCompanyList();
        }


        public void GenerateCompanyList()
        {
            StreamReader sr = new StreamReader(filename);

            for(int i = 0; i < 5; i++)
            {
                string department = departments[i];
                string country = countries[i];
                Departments.Add(new Department(department, country));
            }

            while(!sr.EndOfStream)
            {
                string[] FIO = sr.ReadLine().Split(' ');
                string surname = FIO[0];
                string name = FIO[1];
                string patronymic = FIO[2];
                bool trainee = rnd.Next(0, 2) == 0 ? false : true;
                Department department = Departments[rnd.Next(0, Departments.Count)];
                Employees.Add(new Employee(name, surname, patronymic, trainee, department));
            }
            sr.Close();
        }
    }
}
