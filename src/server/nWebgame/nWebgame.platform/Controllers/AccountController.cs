using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using nWebgame.platform.Controllers.Models;
using nWebgame.platform.DB;
using nWebgame.platform.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace nWebgame.platform.Controllers
{
    /// <summary>
    /// 平台的控制器
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public Response<string> CreateAccount([FromBody] CreateAccountRequest request)
        {
            _logger.LogInformation($"request {request.Name} {request.Password}");

            using (var db = new DBSet())
            {
                var exits = db.PlatformAccounts.Where(o => o.Name == request.Name).FirstOrDefault();
                if(exits == null)
                {
                    exits = new Entitys.PlatformAccount 
                    { 
                        Name = request.Name,
                        Password = request.Password,
                    };

                    db.PlatformAccounts.Add(exits);
                    db.SaveChanges();
                }
            }

            return new Response<string>();
        }

  
    }
}
