using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserCenter.Services.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreateDateTime { get; set; } = DateTime.Now;
    }
}
