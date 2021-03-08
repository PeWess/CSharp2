using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_2
{
    public abstract class Worker : ISorter
    {
        #region Private Fields

        protected string _firstName;
        protected string _secondName;
        protected string _id;
        protected double _payment;


        #endregion

        #region Constructors

        protected Worker(string firstName, string secondName, string id, double payment)
        {
            _firstName = firstName;
            _secondName = secondName;
            _id = id;
            _payment = payment;
        }

        #endregion

        #region Methods

        public abstract double MonthlyPayment();

        public abstract void PrintInfo();

        public int SorterByName(Worker a, Worker b)
        {
            if (a._firstName[0] > b._firstName[0]) return 1;
            if (a._firstName[0] < b._firstName[0]) return -1;
            return 0;
        }

        public int SorterByPay(Worker a, Worker b)
        {
            if (a.MonthlyPayment() > b.MonthlyPayment()) return 1;
            if (a.MonthlyPayment() < b.MonthlyPayment()) return -1;
            return 0;
        }

        #endregion
    }
}
