using Company.Controls;
using CompanyCommunication.CompanyService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_5
{
    public class CompanyDatabase
    {
        private CompanyServiceSoapClient companyServiceSoapClient = new CompanyServiceSoapClient();

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

        public ObservableCollection<Employee> Employees { get; set; }
        public ObservableCollection<Department> Departments { get; set; }
        public List<int> Identifiers { get; set; }

        public CompanyDatabase()
        {
            Employees = new ObservableCollection<Employee>();
            Departments = new ObservableCollection<Department>();
            Identifiers = new List<int>();
            LoadFromDatabase();
        }

        public int AddEmployee(Employee employee)
        {
            var result = companyServiceSoapClient.AddEmployee(employee);
            if (result > 0)
            {
                Employees.Add(employee);
            }
            return result;
        }

        public int AddDepartment(Department department)
        {
            var result = companyServiceSoapClient.AddDepartment(department);
            if (result > 0)
            {
                Departments.Add(department);
            }
            return result;
        }

        public void LoadFromDatabase()
        {
            foreach (var employee in companyServiceSoapClient.LoadEmployees())
            {
                Employees.Add(employee);
            }

            foreach (var department in companyServiceSoapClient.LoadDepartments())
            {
                Departments.Add(department);
            }
        }

        public int Remove(Employee employee)
        {
            var result = companyServiceSoapClient.Remove(employee);
            if (result > 0)
            {
                Employees.Remove(employee);
            }
            return result;
        }

        public int Update(Employee employee)
        {
            return companyServiceSoapClient.Update(employee);
        }

        public void GenerateCompanyList()
        {
            StreamReader sr = new StreamReader(filename);

            for (int i = 0; i < 5; i++)
            {
                string department = departments[i];
                string country = countries[i];
                Departments.Add(new Department(department, country));
            }

            while (!sr.EndOfStream)
            {
                int id = rnd.Next(1000, 10000);
                while (Identifiers.Contains(id) == true)
                {
                    id = rnd.Next(1000, 10000);
                }
                Identifiers.Add(id);
                string[] FIO = sr.ReadLine().Split(' ');
                string surname = FIO[0];
                string name = FIO[1];
                string patronymic = FIO[2];
                bool trainee = rnd.Next(0, 2) == 0 ? false : true;
                Department department = Departments[rnd.Next(0, Departments.Count)];
                Employees.Add(new Employee(id, name, surname, patronymic, trainee, department));
            }
            sr.Close();
        }
    }
}
