using System;

namespace nWebgame.platform.Controllers.Models
{

    /// <summary>
    /// 创建账号的请求
    /// </summary>
    public class CreateAccountRequest
    {
        public string Name { get; set; }

        public string Password { get; set; }
    }
}