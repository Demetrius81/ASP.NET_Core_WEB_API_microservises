using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson1.Model
{
    /// <summary>
    /// Репозиторий для хранения пары дата - температура и доступа к ним
    /// </summary>
    public class TempRepo
    {
        /// <summary>
        /// Поле для хранения пары дата - температура
        /// </summary>
        private Dictionary<DateTime, decimal> _dateAndTemp;

        /// <summary>
        /// Свойство доступа к полю для хранения пары дата - температура
        /// </summary>
        public Dictionary<DateTime, decimal> DateAndTemp { get => _dateAndTemp; set => _dateAndTemp = value; }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public TempRepo()
        {
            _dateAndTemp = new Dictionary<DateTime, decimal>();
        }

        /// <summary>
        /// Метод добавляет пару дата - температура в репозиторий
        /// </summary>
        /// <param name="date">DateTime дата и время</param>
        /// <param name="temp">decimal температура</param>
        public void Add(DateTime date, decimal temp)
        {
            _dateAndTemp.Add(date, temp);
        }

        /// <summary>
        /// Метод возвращает температуру по дате
        /// </summary>
        /// <param name="date">DateTime дата</param>
        /// <returns>decimal температура</returns>
        public decimal Get(ref DateTime date)
        {
            _ = _dateAndTemp.TryGetValue(date, out decimal temp);

            return temp;
        }

        /// <summary>
        /// Метод возвращает всю коллекцию значений дата - температура
        /// </summary>
        /// <returns>string вся коллекция значений дата - температура</returns>
        public string GetAll()
        {
            string result = string.Empty;

            foreach (DateTime key in _dateAndTemp.Keys)
            {
                result = result + $"{key}" + ", " + $"_DateAndTemp[key]" + "; ";
            }

            return result;
        }

        /// <summary>
        /// Метод изменяет температуру по дате
        /// </summary>
        /// <param name="date">DateTime дата и время</param>
        /// <param name="temp">decimal температура</param>
        public void Replace(DateTime date, decimal temp)
        {
            if (_dateAndTemp.ContainsKey(date))
            {
                _dateAndTemp[date] = temp;
            }
        }

        /// <summary>
        /// Метод удаляет температуру в указанный интервал времени
        /// </summary>
        /// <param name="dateTimeStart">DateTime начало интервала</param>
        /// <param name="dateTimeEnd">DateTime конец интервала</param>
        public void RemoveInterval(DateTime dateTimeStart, DateTime dateTimeEnd)
        {
            foreach (DateTime key in GetInterval(dateTimeStart, dateTimeEnd))
            {
                _ = _dateAndTemp.Remove(key);
            }
        }

        /// <summary>
        /// Метод возвращает коллекцию значений дата - температура за указанный промежуток времени
        /// </summary>
        /// <param name="dateTimeStart">DateTime начало интервала</param>
        /// <param name="dateTimeEnd">DateTime конец интервала</param>
        /// <returns>string коллекция значений дата - температура</returns>
        public string ReadInterval(DateTime dateTimeStart, DateTime dateTimeEnd)
        {
            string result = string.Empty;

            foreach (DateTime key in GetInterval(dateTimeStart, dateTimeEnd))
            {
                result = result + $"{key}" + ", " + $"_DateAndTemp[key]" + "; ";
            }

            return result;
        }

        /// <summary>
        /// Метод возвращает коллекцию ключей в интервале времени
        /// </summary>
        /// <param name="dateTimeStart">DateTime начало интервала</param>
        /// <param name="dateTimeEnd">DateTime конец интервала</param>
        /// <returns></returns>
        private List<DateTime> GetInterval(DateTime dateTimeStart, DateTime dateTimeEnd) =>
            _dateAndTemp.Keys.Where(key => key >= dateTimeStart && key <= dateTimeEnd).ToList();
    }
}
