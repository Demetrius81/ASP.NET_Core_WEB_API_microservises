using System;
using System.Collections.Generic;

namespace Lesson1.Model
{
    /// <summary>
    /// Репозиторий для хранения пары дата - температура и доступа к ним
    /// </summary>
    public class TempRepo
    {
        private Dictionary<DateTime, decimal> _DateAndTemp;

        /// <summary>
        /// Свойство доступа к полю для хранения пары дата - температура
        /// </summary>
        public Dictionary<DateTime, decimal> DateAndTemp { get => _DateAndTemp; set => _DateAndTemp = value; }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public TempRepo()
        {
            _DateAndTemp = new Dictionary<DateTime, decimal>();
        }

        /// <summary>
        /// Метод добавляет пару дата - температура в репозиторий
        /// </summary>
        /// <param name="date">DateTime дата и время</param>
        /// <param name="temp">decimal температура</param>
        public void Add(DateTime date, decimal temp)
        {
            _DateAndTemp.Add(date, temp);
        }

        /// <summary>
        /// Метод возвращает температуру по дате
        /// </summary>
        /// <param name="date">DateTime дата</param>
        /// <returns>decimal температура</returns>
        public decimal Get(ref DateTime date)
        {
            _ = _DateAndTemp.TryGetValue(date, out decimal temp);
           
            return temp;
        }
    }
}
