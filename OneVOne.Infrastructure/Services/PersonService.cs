using OneVOne.Core.Entities;
using OneVOne.Infrastructure.Repositories;
using OneVOne.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneVOne.Infrastructure.Services
{
    public class PersonService : IPersonService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PersonService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Person> GetPersonAsync(Guid id)
        {
            return await _unitOfWork.PersonRepository.FindAsync(id);
        }
    }
}
