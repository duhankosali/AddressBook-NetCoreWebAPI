using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AddressBook.Core.DTOs
{
    public class ResponseDto<T>
    {
        // response success ctors:
        public static ResponseDto<T> Success(int statusCode, T data, string message)
        {
            return new ResponseDto<T>
            {
                Data = data,
                StatusCode = statusCode,
                Message = new List<string> { message }
            };
        }
        public static ResponseDto<T> Success(int statusCode, string message)
        {
            return new ResponseDto<T>
            {
                StatusCode = statusCode,
                Message = new List<string> { message }
            };
        }

        // response error ctors:
        public static ResponseDto<T> Fail(int statusCode, List<string> errors)
        {
            return new ResponseDto<T>
            {
                StatusCode = statusCode,
                Message = errors
            };
        }
        public static ResponseDto<T> Fail(int statusCode, string error)
        {
            return new ResponseDto<T>
            {
                StatusCode = statusCode,
                Message = new List<string> { error }
            };
        }

        // properties
        public T Data { get; set; } // data (nullable)
        public List<String> Message { get; set; } // errors
        [JsonIgnore]
        public int StatusCode { get; set; }
    }
}
