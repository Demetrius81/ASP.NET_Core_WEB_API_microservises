using System;
using System.Collections.Generic;

namespace MetricsManager.Services.Interfaces
{
    public interface IRepository<TKey, TValue> where TKey : struct
    {
        /// <summary>
        /// Метод возвращает коллекцию всех метрик
        /// </summary>
        /// <returns></returns>
        IDictionary<TKey, TValue> Get();

        /// <summary>
        /// Метод возвращает метрику по ее ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TValue GetById(TKey id);

        /// <summary>
        /// Метод добавляет метрику в коллекцию
        /// </summary>
        /// <param name="entity"></param>
        void Add(TValue entity);

        /// <summary>
        /// Метод обновляет метрику в коллекции
        /// </summary>
        /// <param name="entity"></param>
        void Update(TValue entity);

        /// <summary>
        /// Метод удаляет метрику по ее ID из коллекции
        /// </summary>
        /// <param name="id"></param>
        void Remove(TKey id);
    }
}
