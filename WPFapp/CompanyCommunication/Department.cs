using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyCommunication.CompanyService
{
    public partial class Department : ICloneable
    {
        public Department() { }

        public Department(string department, string country)
        {
            Dep = department;
            Country = country;
        }

        public object Clone()
        {
            return this.MemberwiseClone(); ;
        }
    }
}
