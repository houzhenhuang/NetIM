using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserCenter.Services.Entities
{
    public class AppInfoEntity: BaseEntity
    {
        public string Name { get; set; }
        public string AppKey { get; set; }
        public string AppSecret { get; set; }
        public bool IsEnabled { get; set; }
    }
}
