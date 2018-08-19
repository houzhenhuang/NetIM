using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace UserCenter.NetSDK
{
    public class SDKResult
    {
        /// <summary>
        /// 响应报文本
        /// </summary>
        public string Result { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
