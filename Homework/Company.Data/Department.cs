using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Company.Data
{
    public class Department : INotifyPropertyChanged, ICloneable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _dep;
        private string _country;

        public string Dep
        {
            get { return _dep; }

            set
            {
                _dep = value;
                NotifyPropertyChanged();
            }
        }

        public string Country
        {
            get { return _country; }

            set
            {
                _country = value;
                NotifyPropertyChanged();
            }
        }

        public string DepLocation
        {
            get
            {
                return _dep + " - " + _country;
            }
        }

        public Department() { }

        public Department(string department, string country)
        {
            Dep = department;
            Country = country;
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
