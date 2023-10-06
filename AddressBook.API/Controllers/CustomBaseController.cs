using AddressBook.Core.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook.API.Controllers
{
    public class CustomBaseController : ControllerBase
    {
        [NonAction] // not endpoint
        public IActionResult CreateActionResult<T>(ResponseDto<T> response)
        {
            if(response.StatusCode == 204) // 204 --> no content
            {
                return new ObjectResult(null)
                {
                    StatusCode = response.StatusCode,
                };
            }

            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }
    }
}
