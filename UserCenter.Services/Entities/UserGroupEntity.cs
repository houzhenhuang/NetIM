using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserCenter.Services.Entities
{
    public class UserGroupEntity : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<UserEntity> Users { get; set; } = new List<UserEntity>();
    }
}
