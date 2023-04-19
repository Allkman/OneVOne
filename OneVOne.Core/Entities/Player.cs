using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneVOne.Core.Entities
{
    public class Player : EntityId
    {
        public string? Position { get; set; }
        public byte? OutsideScoring { get; set; }
        public byte? InsideScoring { get; set; }
        public byte? Defending { get; set; }
        public byte? Athleticism { get; set; }
        public byte? Playmaking { get; set; }
        public byte? Rebounding { get; set; }
        public bool? IsAttacker { get; set; }
        public Guid? PersonId { get; set; }
        public Guid? TeamId { get; set; }
        public string? NbaPlayerPageUrl { get; set; }
        public virtual Person Person { get; set; }
        public virtual PlayerImage PlayerImage { get; set; }

    }
}
