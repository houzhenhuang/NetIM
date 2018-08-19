using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserCenter.NetSDK.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            //UserApi userApi = new UserApi("fasdf2236afasdZ98", "fsadfa$900jiuy7832yhuXz", "http://127.0.0.1:8888/api/v1/");
            //var id=userApi.AddNew("133","lily","123").GetAwaiter().GetResult();
            //Console.WriteLine(id);

            //UserGroupApi userGroupApi = new UserGroupApi("fasdf2236afasdZ98", "fsadfa$900jiuy7832yhuXz", "http://127.0.0.1:8888/api/v1/");
            //var groups = userGroupApi.GetGroups(1).GetAwaiter().GetResult();
            //foreach (var item in groups)
            //{
            //    Console.WriteLine(item.Name);
            //}

            UserApi userApi = new UserApi("fasdf2236afasdZ98", "fsadfa$900jiuy7832yhuXz", "http://127.0.0.1:8888/api/v1/");
            var user = userApi.GetById(1).GetAwaiter().GetResult();
            Console.WriteLine(user.NickName);


            Console.ReadKey();
        }
    }
}
