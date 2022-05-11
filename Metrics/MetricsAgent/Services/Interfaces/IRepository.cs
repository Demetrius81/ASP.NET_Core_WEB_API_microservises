using System;
using System.Collections.Generic;

namespace MetricsAgent.Services.Interfaces
{
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Метод возвращает коллекцию всех метрик
        /// </summary>
        /// <returns></returns>
        IList<T> GetAll();

        /// <summary>
        /// Метод возвращает коллекцию метрик за указанный промежуток времени
        /// </summary>
        /// <param name="fromTime"></param>
        /// <param name="toTime"></param>
        /// <returns></returns>
        IList<T> GetByTimePeriod(TimeSpan fromTime, TimeSpan toTime);

        /// <summary>
        /// Метод возвращает метрику по ее ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetById(int id);

        /// <summary>
        /// Метод добавляет метрику в коллекцию
        /// </summary>
        /// <param name="entity"></param>
        void Create(T entity);

        /// <summary>
        /// Метод обновляет метрику в коллекции
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);

        /// <summary>
        /// Метод удаляет метрику по ее ID из коллекции
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);
    }
}
