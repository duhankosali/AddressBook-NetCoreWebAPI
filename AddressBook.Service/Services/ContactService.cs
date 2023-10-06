using AddressBook.Core;
using AddressBook.Core.DTOs;
using AddressBook.Core.Repositories;
using AddressBook.Core.Services;
using AddressBook.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Service.Services
{
    public class ContactService : Service<Contact>, IContactService
    {
        private readonly IContactRepository _contactRepository;
        public ContactService(IGenericRepository<Contact> repository, IContactRepository contactRepository, IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {
            _contactRepository = contactRepository;
        }

        public async Task<IEnumerable<Contact>> SearchAsync(SearchRequestDto request)
        {
            return await _contactRepository.SearchAsync(request);
        }

        public async Task<bool> IsContactExist(Contact contact)
        {
            return await _contactRepository.AnyAsync(x =>
                x.Name.ToLower() == contact.Name.ToLower() &&
                x.Phone == contact.Phone ||
                x.Email.ToLower() == contact.Email.ToLower());
        }
    }
}
