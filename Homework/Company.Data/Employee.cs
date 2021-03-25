using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Data
{
    public class Employee
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public bool Trainee { get; set; }

        public string Dep { get; set; }

        public string FIO
        {
            get
            {
                return Surname + " " + Name + " " +  Patronymic;
            }
        }

        public Employee() { }

        public Employee(string name, string surname, string patronymic, bool trainee, string dep)
        {
            Name = name;
            Surname = surname;
            Patronymic = patronymic;
            Trainee = trainee;
            Dep = dep;
        }
    }
}
