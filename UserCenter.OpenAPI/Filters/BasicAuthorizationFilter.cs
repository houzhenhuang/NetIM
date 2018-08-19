using RuPeng.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using UserCenter.IServices;

namespace UserCenter.OpenAPI.Filters
{
    public class BasicAuthorizationFilter : IAuthorizationFilter
    {
        public IAppInfoService AppInfoService { get; set; }

        public bool AllowMultiple => true;

        public async Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            IEnumerable<string> appKeys;
            if (!actionContext.Request.Headers.TryGetValues("AppKey", out appKeys))
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized) { Content = new StringContent("报文头中的AppKey为空！") };
            }
            IEnumerable<string> signs;
            if (!actionContext.Request.Headers.TryGetValues("Sign", out signs))
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized) { Content = new StringContent("报文头中的Sign为空！") };
            }
            string appKey = appKeys.FirstOrDefault();
            string sign = signs.FirstOrDefault();
            var appInfo = await AppInfoService.GetByAppKeyAsync(appKey);
            if (appInfo == null)
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized) { Content = new StringContent("不存在的AppKey！") };
            }
            if (!appInfo.IsEnabled)
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden) { Content = new StringContent("AppKey已经被封禁！") };
            }
            //计算用户输入参数的连接+AppSecret的MD5值
            //orderQS就是按照key(参数的名字)进行排序的QueryString集合
            var orderQS = actionContext.Request.GetQueryNameValuePairs().OrderBy(p => p.Key);
            var segments = orderQS.Select(kv => kv.Key + "=" + kv.Value);
            string qs = String.Join("&", segments);
            string computeSign = MD5Helper.ComputeMd5(qs + appInfo.AppSecret);
            if (sign.Equals(computeSign, StringComparison.CurrentCultureIgnoreCase))
            {
                return await continuation();
            }
            else
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden) { Content = new StringContent("Sign验证失败！") };
            }
        }
    }
}