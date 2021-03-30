using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nWebgame.platform.Entitys
{
    /// <summary>
    /// 平台账号
    /// </summary>
    public class Response<T>
    {
        public int Code { get; set; }

        public string Error { get; set; }

        public T Data { get; set; }
    }
}
