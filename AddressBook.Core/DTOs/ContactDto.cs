using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Core.DTOs
{
    public class ContactDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string? MobilePhone { get; set; }
        public string? Email { get; set; }
    }
}
