using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace UserCenter.OpenAPI.Controllers
{
    public class DefaultController : ApiController
    {
        [HttpGet]
        public async Task<string> Get()
        {
            return await Task.FromResult("ok");
        }
    }
}
