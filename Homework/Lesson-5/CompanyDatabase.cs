using Company.Data;
using System;
using System.Collections.Generic;
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

        public List<Employee> Employees{ get; set; }
        public string[] Departments { get; set; }

        public CompanyDatabase()
        {
            Employees = new List<Employee>();
            Departments = Enum.GetNames(typeof(Department));
            GenerateCompanyList();
        }


        public void GenerateCompanyList()
        {
            StreamReader sr = new StreamReader(filename);
            while(!sr.EndOfStream)
            {
                string[] FIO = sr.ReadLine().Split(' ');
                string surname = FIO[0];
                string name = FIO[1];
                string patronymic = FIO[2];
                bool trainee = rnd.Next(0, 2) == 0 ? false : true;
                string department = Departments[rnd.Next(0, Departments.Length)];
                Employees.Add(new Employee(name, surname, patronymic, trainee, department));
            }
            sr.Close();
        }
    }
}
