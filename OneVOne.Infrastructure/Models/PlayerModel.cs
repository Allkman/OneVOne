using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneVOne.Infrastructure.Models
{
    public class PlayerModel : Model
    {
        public Guid PlayerId { get; set; }
        public string? Position { get; set; }
        public byte OutsideScoring { get; set; }
        public byte InsideScoring { get; set; }
        public byte Defending { get; set; }
        public byte Athleticism { get; set; }
        public byte Playmaking { get; set; }
        public byte Rebounding { get; set; }
        public bool IsAttacker { get; set; }
        public PersonModel Person { get; set; }
    }
}
