using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_4
{
    class Program
    {
        static bool Sorter1(int place)
        {
            if (place < 2) return false;
            else return true;
        }
        static bool Sorter2(int place)
        {
            if (place < 3) return false;
            else return true;
        }
        static bool Sorter3(int place)
        {
            if (place < 4) return false;
            else return true;
        }

        static void Main(string[] args)
        {
            List<int> myList = new List<int>();
            Dictionary<int, int> repeatingsOptA = new Dictionary<int, int>();

            Console.WriteLine("Заполните коллекцию 10 целыми числами:");
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    Console.Write($"Элемент {i + 1}: ");
                    myList.Add(int.Parse(Console.ReadLine()));
                }
                catch (FormatException)
                {
                    Console.WriteLine("Вводите ТОЛЬКО целые числа");
                    i--;
                }
            }

            Console.WriteLine("Для продолжения нажмите любую клавишу");
            Console.ReadKey();

            #region Option a

            Console.WriteLine("\nПодсчет количества повторов каждого элемента в коллекции:");
            for (int i = 0; i < 10; i++)
            {
                if (repeatingsOptA.ContainsKey(myList[i]) == false) repeatingsOptA.Add(myList[i], 1);
                else repeatingsOptA[myList[i]]++;
            }

            foreach (var pair in repeatingsOptA)
            {
                Console.WriteLine($"Число повторений элемента {pair.Key} = {pair.Value}");
            }

            Console.WriteLine("Для продолжения нажмите любую клавишу");
            Console.ReadKey();

            #endregion

            #region Option c

            Console.WriteLine("\nПодсчет количества повторов каждого элемента в коллекции с использованием Linq:");
            var repeatingsOptC = myList.GroupBy(x => x)
                .Where(g => g.Count() > 0)
                .Select(y => new { Key = y.Key, Value = y.Count() })
                .ToList();

            foreach (var pair in repeatingsOptC)
            {
                Console.WriteLine($"Число повторений элемента {pair.Key} = {pair.Value}");
            }

            Console.WriteLine("Для продолжения нажмите любую клавишу");
            Console.ReadKey();
            Console.Clear();

            #endregion

            #region b

            ArrayList newList = new ArrayList();

            Console.WriteLine("Заполните коллекцию 10 любыми элементами:");
            for (int i = 0; i < 10; i++)
            {
                Console.Write($"Элемент {i + 1}: ");
                newList.Add(Console.ReadLine());
            }

            Console.WriteLine("\nПодсчет количества повторов каждого элемента в необощенной коллекции:");
            Dictionary<string, int> repeatingsOptB = newList.ToArray().GroupBy(x => x)
                .ToDictionary(g => g.Key.ToString(),
                g => g.Count());

            foreach (var obj in repeatingsOptB)
            {
                Console.WriteLine($"Число повторений элемента {obj.Key} = {obj.Value}");
            }

            Console.WriteLine("Для продолжения нажмите любую клавишу");
            Console.ReadKey();
            Console.Clear();

            #endregion

            #region Task3

            Dictionary<string, int> dict = new Dictionary<string, int>()
            {
                {"four",4 },
                {"two",2 },
                { "one",1 },
                {"three",3 },

            };
            Console.WriteLine("Задание 3. Необходимо отсортировать указанный Dictionary, по-разному обращаясь к OrderBy");
            Console.WriteLine("***Пример***");
            var sortedDict1 = dict.OrderBy(delegate (KeyValuePair<string, int> pair) { return pair.Value; });
            foreach (var pair in sortedDict1)
            {
                Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
            }

            Console.WriteLine("\n***Через лямбда-выражения***");
            var sortedDict2 = dict.OrderBy(pair => pair.Value);
            foreach (var pair in sortedDict2)
            {
                Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
            }

            Console.WriteLine("\n***Через делегаты Predicate<T>***");
            List<Predicate<int>> predicates = new List<Predicate<int>> { new Predicate<int>(Sorter1), new Predicate<int>(Sorter2), new Predicate<int>(Sorter3) };
            var sortedDict3 = dict.OrderBy(pair => predicates[0](pair.Value)).OrderBy(pair => predicates[1](pair.Value)).OrderBy(pair => predicates[2](pair.Value));
            foreach (var pair in sortedDict3)
            {
                Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
            }

            Console.WriteLine("\nДля продолжения нажмите любую клавишу");
            Console.ReadKey();

            #endregion
        }
    }
}
