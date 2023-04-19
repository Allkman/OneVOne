using OneVOne.Core.Entities;
using OneVOne.Infrastructure;
using OneVOne.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OneVOne.Repository
{
    internal sealed class PersonRepository : RepositoryId<Person>, IPersonRepository
    {
        public PersonRepository(DataContext context) : base(context)
        {
        }
    }
}
