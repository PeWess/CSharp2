using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyCommunication.CompanyService
{
    public partial class Employee : ICloneable
    {
        public Employee()
        {
            CurDep = new ArrayOfString();
        }

        public Employee(int id, string name, string surname, string patronymic, bool trainee, Department dep)
        {
            ID = id;
            Name = name;
            Surname = surname;
            Patronymic = patronymic;
            Trainee = trainee;
            CurDep = new ArrayOfString();
            CurDep[0] = dep.Dep;
            CurDep[1] = dep.Country;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
