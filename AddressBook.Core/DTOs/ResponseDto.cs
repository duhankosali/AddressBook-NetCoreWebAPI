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
        public static ResponseDto<T> Success(int statusCode, T data)
        {
            return new ResponseDto<T>
            {
                Data = data,
                StatusCode = statusCode
            };
        }
        public static ResponseDto<T> Success(int statusCode)
        {
            return new ResponseDto<T>
            {
                StatusCode = statusCode
            };
        }

        // response error ctors:
        public static ResponseDto<T> Fail(int statusCode, List<string> errors)
        {
            return new ResponseDto<T>
            {
                StatusCode = statusCode,
                Errors = errors
            };
        }
        public static ResponseDto<T> Fail(int statusCode, string error)
        {
            return new ResponseDto<T>
            {
                StatusCode = statusCode,
                Errors = new List<string> { error }
            };
        }

        // properties
        public T Data { get; set; } // data (nullable)
        public List<String> Errors { get; set; } // errors
        [JsonIgnore]
        public int StatusCode { get; set; }
    }
}
