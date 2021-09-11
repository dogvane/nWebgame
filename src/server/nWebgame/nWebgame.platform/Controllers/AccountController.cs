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
            // _logger.LogInformation($"request {request.Name} {request.Password}");
            // Console.WriteLine($"request {request.Name} {request.Password}");

            using (var db = new DBSet())
            {
                using (var tran = db.Database.BeginTransaction())
                {
                    try
                    {
                        var exits = db.PlatformAccounts.FirstOrDefault(o => o.Name == request.Name);
                        if (exits == null)
                        {
                            exits = new PlatformAccount
                            {
                                Name = request.Name,
                                Password = request.Password,
                            };

                            db.PlatformAccounts.Add(exits);
                            db.SaveChanges();
                            tran.Commit();
                        }
                        else
                        {
                            return Response<string>.Error("账号已存在!");
                        }
                    }
                    catch(Exception ex)
                    {
                        _logger.LogError(ex, "create user fail. {0}", request.Name);
                        tran.Rollback();
                        return Response<string>.Error(ex.Message);
                    }
                }
            }

            return new Response<string>();
        }

  
    }
}
