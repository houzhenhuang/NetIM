using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserCenter.NetSDK.Entity;

namespace UserCenter.NetSDK
{
    public class UserApi
    {
        private string appKey;
        private string appSecret;
        private string serverRoot;
        public UserApi(string appKey, string appSecret, string serverRoot)
        {
            this.appKey = appKey;
            this.appSecret = appSecret;
            this.serverRoot = serverRoot;
        }

        public async Task<long> AddNew(string phoneNum, string nickName, string password)
        {
            SDKClient sdkClient = new SDKClient(appKey, appSecret, serverRoot);
            Dictionary<string, object> queryStringData = new Dictionary<string, object>();
            queryStringData.Add("phoneNum", phoneNum);
            queryStringData.Add("nickName", nickName);
            queryStringData.Add("password", password);
            var result = await sdkClient.GetAsync("User/AddNew", queryStringData);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                long id = JsonConvert.DeserializeObject<long>(result.Result);
                return id;
            }
            else
            {
                throw new ApplicationException("新增失败，状态码：" + result.StatusCode + "响应报文：" + result.Result);
            }
        }

        public async Task<User> GetById(long id)
        {
            SDKClient sdkClient = new SDKClient(appKey, appSecret, serverRoot);
            Dictionary<string, object> queryStringData = new Dictionary<string, object>();
            queryStringData.Add("id", id);
            var result = await sdkClient.GetAsync("User/GetById", queryStringData);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var user = JsonConvert.DeserializeObject<User>(result.Result);
                return user;
            }
            else
            {
                throw new ApplicationException("获取用户失败，状态码：" + result.StatusCode + "响应报文：" + result.Result);
            }
        }

    }
}
