using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Company.Data
{
    public class Employee : INotifyPropertyChanged, ICloneable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _name;

        private string _surname;

        private string _patronymic;

        private bool _trainee;

        private string[] _curDep
            ;
        public string Name
        {
            get { return _name; }

            set
            {
                _name = value;
                NotifyPropertyChanged();
            }
        }

        public string Surname
        {
            get { return _surname; }

            set
            {
                _surname = value;
                NotifyPropertyChanged();
            }
        }

        public string Patronymic
        {
            get { return _patronymic; }

            set
            {
                _patronymic = value;
                NotifyPropertyChanged();
            }
        }
        public bool Trainee
        {
            get { return _trainee; }

            set
            {
                _trainee = value;
                NotifyPropertyChanged();
            }
        }
        public string[] CurDep
        {
            get { return _curDep; }

            set
            {
                _curDep = value;
                NotifyPropertyChanged();
            }
        }
        public string FIO
        {
            get
            {
                return Surname + " " + Name + " " +  Patronymic;
            }
        }

        public string PlaceOfWork
        {
            get
            {
                return CurDep[0] + " - " + CurDep[1];
            }

            set
            {
                if (value != "")
                {
                    string[] str = value.Split('-');
                    CurDep[0] = str[0];
                    CurDep[1] = str[1];
                }
            }
        }

        public Employee() 
        {
            CurDep = new string[2];
        }

        public Employee(string name, string surname, string patronymic, bool trainee, Department dep)
        {
            Name = name;
            Surname = surname;
            Patronymic = patronymic;
            Trainee = trainee;
            CurDep = new string[2];
            CurDep[0] = dep.Dep;
            CurDep[1] = dep.Country;
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
