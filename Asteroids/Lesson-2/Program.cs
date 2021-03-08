using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_2
{
    class Program
    {
        class Office
        {
            private Worker[] office;

            public Office()
            {
                office = new Worker[0];
            }

            public Office(Worker[] mass)
            {
                office = mass;
            }

            public void PrintOfficeInfo()
            {
                foreach(Worker worker in office)
                {
                    worker.PrintInfo();
                }
            }
        }

        static void Main(string[] args)
        {
            Worker[] workers = new Worker[5] { new HourlyPayment("Сергей", "Критский", "144245", 360.53),
            new HourlyPayment("Кирилл", "Ковальчук", "543344", 548.23),
            new FixedPayment("Алина", "Викторова", "237934", 346876.76),
            new FixedPayment("Анастасия", "Васильева", "453323", 158000.36),
            new HourlyPayment("Елена", "Кондратьева", "269457", 234.64)};

            Office database = new Office(workers);

            Console.WriteLine("В каком виде вы хотите просмотреть список сотрудников:\n" +
                "1 - Неотсортированный список\n2 - Список, отсортированный по именам\n3 - Список, отсортирванный по месячной выплате");
            int choice;
            while (int.TryParse(Console.ReadLine(), out choice) == false)
            {
                Console.WriteLine("Неизвестная опция, попробуйте ввести другие данные: ");
            }

            switch (choice)
            {
                case 1:
                    database.PrintOfficeInfo();
                    break;

                case 2:
                    Array.Sort(workers, workers[0].SorterByName);
                    database.PrintOfficeInfo();
                    break;

                case 3:
                    Array.Sort(workers, workers[0].SorterByPay);
                    database.PrintOfficeInfo();
                    break;
            }

            Console.ReadKey();
        }
    }
}