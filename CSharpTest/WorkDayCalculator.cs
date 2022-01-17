using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTest
{
    public class WorkDayCalculator : IWorkDayCalculator
    {
        public DateTime[] listOfWeekEnds (WeekEnd[] weekEnds)//метод сбора всех выходных дней в один общий массив для последующего перебора
        {
            List<DateTime> daysOfWeekEnds = new List<DateTime>(); // создаем список дней
            int number = weekEnds.Length; //получаем количество элементов в массиве
            for(int i = 0; i < number; i++)
            {
                TimeSpan span = weekEnds[i].EndDate.Subtract(weekEnds[i].StartDate);//получаем разницу между началом и концом выходных
                int numberOfDays = span.Days; // получаем эту разницу в днях
                for (int j = 0; j <= numberOfDays; j++)
                    daysOfWeekEnds.Add(weekEnds[i].StartDate.AddDays(j)); // добавляем в список начиная со стартовой даты по одному дню в зависимости от длинны выходных 
            }
            return daysOfWeekEnds.ToArray(); //возвращаем массив выходных
        }
        public DateTime Calculate(DateTime startDate, int dayCount, WeekEnd[] weekEnds)
        {
            if (weekEnds != null)//если есть выходные
            {
                DateTime endDate = startDate.AddDays(dayCount - 1);//получаем конечную дату без учета выходных
                DateTime[] arrayWeekEnds = listOfWeekEnds(weekEnds); //собираем все даты выходных дней в одном массиве
                foreach (var element in arrayWeekEnds)
                {
                    if (element < endDate) //если дата в массиве меньше предварительной конечной даты, то увеличиваем конечную дату на один день
                        endDate = endDate.AddDays(1); //другими словами, если выходной попадает в промежуток между начальной и конечной датой - увеличиваем конечную дату
                }
                return endDate; // возвращаем конечную дату
            }
            else
                return startDate.AddDays(dayCount - 1);//если нет списка выходных, сразу возвращаем конечную дату

        }
    }
}
