using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SiteActivityReporting.Api.Models;
using SiteActivityReporting.Services;
using SiteActivityReporting.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteActivityReporting.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ActivityController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<ActivityController> _logger;
        private readonly IActivityService _activityService;

        public ActivityController(ILogger<ActivityController> logger,
                                    IActivityService activityService)
        {
            _logger = logger;
            _activityService = activityService;
        }

        [HttpPost("{key}")]
        public async Task<IActionResult> Post(string key, [FromBody] AddActivityModel input)
        {
            _activityService.Add(new CreateActivityInput { Key = key, Value = input.Value });
            return Ok();
        }

        [HttpGet("{key}/total")]
        public async Task<IActionResult> GetTotal(string key)
        {
            var res = _activityService.Total(new GetActivityTotalInput { Key = key });
            if (res == null)
                return NotFound();

            var ret = new GetActivityTotalModel { Value = res.Value };
            return Ok(ret);
        }
    }
}
