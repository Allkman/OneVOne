using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneVOne.GameService.Repository
{
    public sealed class UnitOfWorkOptions
    {
        public string ConnectionString { get; set; }
        public int? Timeout { get; set; }
    }
}
