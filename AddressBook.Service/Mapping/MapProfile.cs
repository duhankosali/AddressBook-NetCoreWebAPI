using AddressBook.Core;
using AddressBook.Core.DTOs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Service.Mapping
{
    public class MapProfile : Profile // Automapper entegration.
    {
        public MapProfile()
        {
            CreateMap<Contact, ContactDto>().ReverseMap();
        }
    }
}
