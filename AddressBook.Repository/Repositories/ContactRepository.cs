using AddressBook.Core;
using AddressBook.Core.DTOs;
using AddressBook.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Repository.Repositories
{
    public class ContactRepository : GenericRepository<Contact>, IContactRepository
    {
        public ContactRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Contact>> SearchAsync(SearchRequestDto request)
        {
            var query = _context.Contacts.AsQueryable();

            if (!string.IsNullOrEmpty(request.Name))
                query = query.Where(c => EF.Functions.ILike(c.Name, $"%{request.Name}%"));

            if (!string.IsNullOrEmpty(request.Address))
                query = query.Where(c => EF.Functions.ILike(c.Address, $"%{request.Address}%"));

            if (!string.IsNullOrEmpty(request.Phone))
                query = query.Where(c => EF.Functions.ILike(c.Phone, $"%{request.Phone}%"));

            if (!string.IsNullOrEmpty(request.Email))
                query = query.Where(c => EF.Functions.ILike(c.Email, $"%{request.Email}%"));

            return await query.ToListAsync();
        }
    }
}
