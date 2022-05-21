﻿using System.Collections.Generic;

namespace Source.Models.Response
{
    public abstract class AllMetricsResponse<T> where T : class
    {
        /// <summary>
        /// Коллекция метрик, сформированная для ответа контроллера
        /// </summary>
        public List<T> Metrics { get; set; }
    }
}
