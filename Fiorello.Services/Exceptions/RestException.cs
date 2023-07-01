using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Fiorello.Services.Exceptions
{
    public class RestException:Exception
    {
        public RestException(HttpStatusCode code,string errorKey,string errorMessage,string message=null)
        {
            StatusCode = code;
            Message = message;
            Errors = new List<RestExceptionError> { new RestExceptionError (errorKey,errorMessage ) };
        }

        public RestException(HttpStatusCode code,List<RestExceptionError> errorList,string message=null)
        {
            StatusCode = code;
            Message = message; 
            Errors = errorList;
        }

        public RestException(HttpStatusCode statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
            Errors = new List<RestExceptionError> { };
        }

        public HttpStatusCode StatusCode {  get; set; }

        public string Message { get; set; }

        public List<RestExceptionError> Errors { get; set; }
    }
    public class RestExceptionError
    {
        public RestExceptionError(string key,string message)
        {
            Key = key;
            Message = message;
        }
        public string Key { get; set; }

        public string Message { get; set; }
    }
}
