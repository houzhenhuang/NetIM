using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserCenter.Services.Entities
{
    public class UserEntity : BaseEntity
    {
        public string PhoneNum { get; set; }
        public string NickName { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }

        public virtual ICollection<UserGroupEntity> Groups { get; set; } = new List<UserGroupEntity>();
    }
}
