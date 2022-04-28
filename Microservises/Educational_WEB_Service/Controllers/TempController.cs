using Lesson1.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Lesson1.Controllers
{
    /*
    
     V Написать свой контроллер и методы в нем, которые бы предоставляли следующую функциональность:
     V Возможность сохранить температуру в указанное время
     V Возможность отредактировать показатель температуры в указанное время
     V Возможность удалить показатель температуры в указанный промежуток времени
     V Возможность прочитать список показателей температуры за указанный промежуток времени

     */

    /// <summary>
    /// Контроллер TempController
    /// </summary>
    [Route("api/temperature")]
    [ApiController]
    public class TempController : ControllerBase
    {
        /// <summary>
        /// Поле для хранения пары дата - температура и доступа к ним
        /// </summary>
        private readonly TempRepo _tempRepo;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="tempRepo"></param>
        public TempController(TempRepo tempRepo)
        {
            _tempRepo = tempRepo;
        }

        /*  
         *  
         * Столкнулся с проблеммой. Если ставлю тип переменной для ввода времени DateTime,
         * выдает ошибку 400. С int все работает.
         * 
         */

        /// <summary>
        /// Метод добавляет запись о температуре в определенное время
        /// </summary>
        /// <param name="dateTime">DateTime время</param>
        /// <param name="temp">decimal температура</param>
        /// <returns></returns>
        [HttpPost("create")]
        public IActionResult Create([FromQuery] decimal temp, [FromQuery] int dateTime)
        {
            #region For best times

            //[FromQuery] DateTime? dateTime = null

            //if (!dateTime.HasValue)
            //{
            //    dateTime = DateTime.UtcNow;
            //}
            //else
            //{
            //    dateTime = DateTime.SpecifyKind(dateTime.Value, DateTimeKind.Utc);
            //}

            #endregion

            _tempRepo.Add(dateTime, temp);

            return Ok();
        }

        /// <summary>
        /// Метод предоставляет запись о температуре по дате
        /// </summary>
        /// <param name="dateTime">DateTime дата</param>
        /// <returns></returns>
        [HttpGet("read")]
        public IActionResult Read([FromQuery] int dateTime)
        {
            decimal temp = _tempRepo.Get(ref dateTime);

            return Ok($"{dateTime}, {temp}");
        }

        /// <summary>
        /// Метод выводит список показателей температуры за указанный промежуток времени
        /// </summary>
        /// <param name="dateTimeStart">DateTime начало интервала</param>
        /// <param name="dateTimeEnd">DateTime конец интервала</param>
        /// <returns></returns>
        [HttpGet("readfortheinterval")]
        public IActionResult ReadForTheIntervale([FromQuery] int dateTimeStart, [FromQuery] int dateTimeEnd)
        {
            return Ok($"{_tempRepo.ReadInterval(dateTimeStart, dateTimeEnd)}");
        }

        /// <summary>
        /// Метод предоставляет все записи о температуре
        /// </summary>
        /// <returns></returns>
        [HttpGet("readall")]
        public IActionResult ReadAll()
        {
            return Ok(_tempRepo.GetAll());
        }

        /// <summary>
        /// Метод редактирует значение температуры по дате
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="temp"></param>
        /// <returns></returns>
        [HttpPut("update")]
        public IActionResult Update([FromQuery] int dateTime, [FromQuery] decimal temp)
        {
            _tempRepo.Replace(dateTime, temp);

            return Ok();
        }

        /// <summary>
        /// Метод удаляет записm о температуре по указанному времени
        /// </summary>
        /// <param name="dateTime">DateTime дата и время</param>
        /// <returns></returns>
        [HttpDelete("delete")]
        public IActionResult Delete([FromQuery] int dateTime)
        {
            _ = _tempRepo.DateAndTemp.Remove(dateTime);

            return Ok();
        }

        /// <summary>
        /// Метод удаляет показатель температуры в указанный промежуток времени
        /// </summary>
        /// <param name="dateTimeStart">DateTime начало интервала</param>
        /// <param name="dateTimeEnd">DateTime конец интервала</param>
        /// <returns></returns>
        [HttpDelete("deletefortheinterval")]
        public IActionResult DeleteForTheIntervale([FromQuery] int dateTimeStart, [FromQuery] int dateTimeEnd)
        {
            _tempRepo.RemoveInterval(dateTimeStart, dateTimeEnd);

            return Ok();
        }

        /// <summary>
        /// Метод удаляет все записи о температуре и времени
        /// </summary>
        /// <returns></returns>
        [HttpDelete("clear")]
        public IActionResult Clear()
        {
            _tempRepo.DateAndTemp.Clear();

            return Ok();
        }
    }
}
