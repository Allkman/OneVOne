using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneVOne.GameService.Infrastructure.Models
{
    public class PlayByPlayStatisticsModel : Model
    {
        public Guid PlayerId { get; set; }
        public byte Score { get; set; }
        public int Steal { get; set; }
        public int Rebound { get; set; }
        public int Block { get; set; }
        public int Foul { get; set; }
    }
}
