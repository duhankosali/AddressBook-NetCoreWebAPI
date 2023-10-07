using AddressBook.Core;
using AddressBook.Core.DTOs;
using AddressBook.Core.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : CustomBaseController
    {
        // dependency injection
        private readonly IMapper _autoMapper; // automapper
        private readonly IContactService _contactService; // custom service
        public ContactsController(IMapper autoMapper, IContactService contactService)
        {
            _autoMapper = autoMapper;
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() // --> GET /api/contacts
        {
            var contacts = await _contactService.GetAllAsync(); // get data
            var contactsDto = _autoMapper.Map<List<ContactDto>>(contacts.ToList()); // mapping
            return CreateActionResult(ResponseDto<List<ContactDto>>.Success(200, contactsDto, "All users are listed."));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) // --> GET /api/contacts/1
        {
            var contact = await _contactService.GetByIdAsync(id); // get data
            var contactDto = _autoMapper.Map<ContactDto>(contact); // mapping
            return CreateActionResult(ResponseDto<ContactDto>.Success(200, contactDto, "Contact person listed."));
        }

        [HttpPost]
        public async Task<IActionResult> Create(ContactDto contactDto) // --> POST /api/contacts
        {
            bool nameControl = await _contactService.AnyAsync(x => x.Name == contactDto.Name);
            if (nameControl)
            {
                return CreateActionResult(ResponseDto<ContactDto>.Fail(404, "There cannot be 2 contacts with the same name."));
            }

            await _contactService.AddAsync(_autoMapper.Map<Contact>(contactDto)); // get data
            return CreateActionResult(ResponseDto<ContactDto>.Success(201, contactDto, "New contact added.")); // 201 --> CREATED
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateContactDto contactDto) // --> PUT /api/contacts
        {
            bool nameControl = await _contactService.AnyAsync(x=>x.Name == contactDto.Name);
            if(nameControl)
            {
                return CreateActionResult(ResponseDto<ContactDto>.Fail(404, "There cannot be 2 contacts with the same name."));
            }

            bool existsControl = await _contactService.AnyAsync(x=>x.Id == contactDto.Id);
            if (!existsControl)
            {
                return CreateActionResult(ResponseDto<ContactDto>.Fail(404, "User not found."));
            }

            await _contactService.UpdateAsync(_autoMapper.Map<Contact>(contactDto));
            return CreateActionResult(ResponseDto<UpdateContactDto>.Success(200, "Contact updated."));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) // --> DELETE /api/contacts
        {
            var contact = await _contactService.GetByIdAsync(id);
            if (contact == null)
            {
                return CreateActionResult(ResponseDto<ContactDto>.Fail(404, "User not found.")); 
            }

            await _contactService.DeleteAsync(contact);
            return CreateActionResult(ResponseDto<ContactDto>.Success(200, "Contact deleted."));
        }

        [HttpGet("search")] // --> GET /api/contacts/search
        public async Task<IActionResult> Search([FromQuery] SearchRequestDto request)
        {
            var contacts = await _contactService.SearchAsync(request);
            var contactsDto = _autoMapper.Map<List<ContactDto>>(contacts);

            return CreateActionResult(ResponseDto<List<ContactDto>>.Success(200, contactsDto, "Filtered users are listed."));
        }
    }
}
