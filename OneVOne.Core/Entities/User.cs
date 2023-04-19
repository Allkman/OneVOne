using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneVOne.Core.Entities
{
    public class User  : EntityId
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
