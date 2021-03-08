using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_2
{
    interface ISorter
    {
        int SorterByPay(Worker a, Worker b);

        int SorterByName(Worker a, Worker b);
    }
}
