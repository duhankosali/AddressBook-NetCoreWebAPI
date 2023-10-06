using AddressBook.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Core.Repositories
{
    public interface IContactRepository : IGenericRepository<Contact>
    {
        Task<IEnumerable<Contact>> SearchAsync(SearchRequestDto request);
    }
}
