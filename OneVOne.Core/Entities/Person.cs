using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneVOne.Core.Entities
{
    public class Person : EntityId
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
