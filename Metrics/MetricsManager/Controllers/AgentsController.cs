using AutoMapper;
using MetricsManager.Models;
using MetricsManager.Models.Interfaces;
using MetricsManager.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace MetricsManager.Controllers
{
    /// <summary>
    /// Контроллер агентов
    /// </summary>
    [Route("api/agents")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly IAgentsPoolRepository _agentsPoolRepository;

        private readonly ILogger<AgentsController> _logger;

        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="agentPool"></param>
        public AgentsController(
            IAgentsPoolRepository agentsPoolRepository,
            IMapper mapper = null,
            ILogger<AgentsController> logger = null)
        {
            _agentsPoolRepository = agentsPoolRepository;

            _mapper = mapper;

            _logger = logger;
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
                _agentsPoolRepository.Add(_mapper.Map<AgentInfoDto>(agentInfo));
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
            Dictionary<int, AgentInfoDto> agentsRepo = (Dictionary<int, AgentInfoDto>)_agentsPoolRepository.Get();
           
            if (agentsRepo.ContainsKey(agentId))
            {
                agentsRepo[agentId].Enable = true;

                _agentsPoolRepository.Update(agentsRepo[agentId]);
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
            Dictionary<int, AgentInfoDto> agentsRepo = (Dictionary<int, AgentInfoDto>)_agentsPoolRepository.Get();

            if (agentsRepo.ContainsKey(agentId))
            {
                agentsRepo[agentId].Enable = false;

                _agentsPoolRepository.Update(agentsRepo[agentId]);
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
            Dictionary<int, AgentInfoDto> agentsRepo = (Dictionary<int, AgentInfoDto>)_agentsPoolRepository.Get();

            if (agentsRepo.ContainsKey(agentId))
            {
                agentsRepo[agentId].Enable =
                    agentsRepo[agentId].Enable == false ? true : false;

                _agentsPoolRepository.Update(agentsRepo[agentId]);
            }
            return Ok(agentsRepo[agentId].Enable);// На интерфейс пользователя можно прикрутить лампочку (какой-нибудь switcher, radiobutton) и она будет показывать состояние агента
        }

        /// <summary>
        /// Метод возвращает список всех агентов)
        /// </summary>
        /// <returns></returns>
        [HttpGet("get")]
        public IActionResult GetAllAgents()
        {
            List<AgentInfoDto> agents = _agentsPoolRepository.Get().Values.ToList();

            List<AgentInfo> result = new List<AgentInfo>();

            foreach (AgentInfoDto agent in agents)
            {
                result.Add(_mapper.Map<AgentInfo>(agent));
            }
            return Ok(result.ToArray());
        } 
    }
}
