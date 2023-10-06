using AddressBook.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Core.Services
{
    public interface IContactService : IService<Contact>
    {
        Task<IEnumerable<Contact>> SearchAsync(SearchRequestDto request);
        Task<bool> IsContactExist(Contact contact);
    }
}
