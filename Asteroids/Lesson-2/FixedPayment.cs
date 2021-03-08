using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_2
{
    public class FixedPayment : Worker
    {
        public FixedPayment(string firstName, string secondName, string id, double payment) : base(firstName, secondName, id, payment) { }

        public override double MonthlyPayment()
        {
            return _payment;
        }

        public override void PrintInfo()
        {
            Console.WriteLine($"Сотрудник с фиксированной оплатой {_firstName} {_secondName}. ID: {_id}. Фиксированная месячная оплата {_payment}");
        }
    }
}
