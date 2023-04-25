using OneVOne.GameService.Core.Entities;
using OneVOne.GameService.Infrastructure;
using OneVOne.GameService.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OneVOne.GameService.Repository
{
    internal sealed class PersonRepository : RepositoryId<Person>, IPersonRepository
    {
        public PersonRepository(DataContext context) : base(context)
        {
        }
    }
}
