using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneVOne.Core.Entities
{
    public class PlayerImage : EntityId
    {
        public byte[]? Image { get; set; }
        public Guid? PlayerId { get; set; }
    }
}
