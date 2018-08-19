using RuPeng.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace UserCenter.NetSDK
{
    public class SDKClient
    {
        private string appKey;
        private string appSecret;
        private string serverRoot;
        public SDKClient(string appKey, string appSecret, string serverRoot)
        {
            this.appKey = appKey;
            this.appSecret = appSecret;
            this.serverRoot = serverRoot;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">要请求的地址</param>
        /// <param name="queryStringData">queryStringData参数键值对</param>
        /// <returns></returns>
        public async Task<SDKResult> GetAsync(string url, IDictionary<string, object> queryStringData)
        {
            if (queryStringData == null)
            {
                throw new ArgumentException("参数queryStringData不能为null");
            }
            var orderedQS = queryStringData.OrderBy(kv => kv.Key);
            var qsList = orderedQS.Select(q => q.Key + "=" + q.Value);
            var qsStr = String.Join("&", qsList);
            string sign = MD5Helper.ComputeMd5(qsStr + appSecret);

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("appKey", appKey);
                httpClient.DefaultRequestHeaders.Add("sign", sign);
                var response = await httpClient.GetAsync(serverRoot+url + "?" + qsStr);
                SDKResult skdResult = new SDKResult();
                skdResult.Result = await response.Content.ReadAsStringAsync();
                skdResult.StatusCode = response.StatusCode;
                return skdResult;
            }
        }
    }
}
