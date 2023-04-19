using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneVOne.Core
{
    public abstract class EntityId : Entity
    {
        public Guid? Id { get; set; }
    }
}
