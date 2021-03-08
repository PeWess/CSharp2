using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_2
{
    public class HourlyPayment : Worker
    {
        public HourlyPayment(string firstName, string secondName, string id, double payment) : base(firstName, secondName, id, payment) { }

        public override double MonthlyPayment()
        {
            return 20.8 * 8 * _payment;
        }

        public override void PrintInfo()
        {
            Console.WriteLine($"Сотрудник с почасовой оплатой {_firstName} {_secondName}. ID: {_id}. Почасовая ставка {_payment}");
        }
    }
}
