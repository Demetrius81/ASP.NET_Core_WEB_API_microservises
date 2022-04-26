using MetricsManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers
{
    /// <summary>
    /// Контроллер агентов
    /// </summary>
    [Route("api/agents")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private AgentPool _agentPool;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="agentPool"></param>
        public AgentsController(AgentPool agentPool)
        {
            _agentPool = agentPool;
        }

        /// <summary>
        /// Метод регистрирует агента
        /// </summary>
        /// <param name="agentInfo"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] AgentInfo agentInfo)
        {
            if (agentInfo != null)
            {
                _agentPool.Add(agentInfo);
            }
            return Ok();
        }

        /// <summary>
        /// Метод включает спящего агента
        /// </summary>
        /// <param name="agentId"></param>
        /// <returns></returns>
        [HttpPut("enable/{agentId}")]
        public IActionResult EnableAgentById([FromRoute] int agentId)
        {
            if (_agentPool.AgentsRepo.ContainsKey(agentId))
            {
                _agentPool.AgentsRepo[agentId].Enable = true;
            }
            return Ok();
        }

        /// <summary>
        /// Метод выключает активного агента
        /// </summary>
        /// <param name="agentId"></param>
        /// <returns></returns>
        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgentById([FromRoute] int agentId)
        {
            if (_agentPool.AgentsRepo.ContainsKey(agentId))
            {
                _agentPool.AgentsRepo[agentId].Enable = false;
            }
            return Ok();
        }

        /// <summary>
        /// Метод переключает состояние агента на противоположное
        /// </summary>
        /// <param name="agentId"></param>
        /// <returns></returns>
        [HttpPut("switch/{agentId}")]
        public IActionResult AgentSwitcById([FromRoute] int agentId)
        {
            if (_agentPool.AgentsRepo.ContainsKey(agentId))
            {
                _agentPool.AgentsRepo[agentId].Enable = 
                    _agentPool.AgentsRepo[agentId].Enable == false ? true : false;
            }
            return Ok(_agentPool.AgentsRepo[agentId].Enable);// На интерфейс пользователя можно прикрутить лампочку (какой-нибудь switcher, radiobutton) и она будет показывать состояние агента
        }

        /// <summary>
        /// Метод возвращает список всех агентов)
        /// </summary>
        /// <returns></returns>
        [HttpGet("get")]
        public IActionResult GetAllAgents() => Ok(_agentPool.Get());
    }
}
