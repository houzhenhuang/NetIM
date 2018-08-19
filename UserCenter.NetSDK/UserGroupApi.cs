using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserCenter.NetSDK.Entity;

namespace UserCenter.NetSDK
{
    public class UserGroupApi
    {
        private string appKey;
        private string appSecret;
        private string serverRoot;
        public UserGroupApi(string appKey, string appSecret, string serverRoot)
        {
            this.appKey = appKey;
            this.appSecret = appSecret;
            this.serverRoot = serverRoot;
        }

        public async Task<UserGroup[]> GetGroups(long userId)
        {
            SDKClient sdkClient = new SDKClient(appKey, appSecret, serverRoot);
            Dictionary<string, object> queryStringData = new Dictionary<string, object>();
            queryStringData.Add("userId", userId);
            var result = await sdkClient.GetAsync("UserGroup/GetGroups", queryStringData);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var userGroups = JsonConvert.DeserializeObject<UserGroup[]>(result.Result);
                return userGroups;
            }
            else
            {
                throw new ApplicationException("GetGroups失败，状态码：" + result.StatusCode + "响应报文：" + result.Result);
            }
        }
    }
}
