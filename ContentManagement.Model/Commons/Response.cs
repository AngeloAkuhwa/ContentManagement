using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContentManagement.Domain.Commons
{
    public class Response<T>
    {
        public Response(string message, T details = default, IList<Error> error = null)
        {
            Message = message;
            Data = details;
            Errors = error;
        }
        public Response()
        {

        }

        public bool Success { get; set; }
        public T Data { get; set; }
        public IList<Error> Errors { get; set; }
        public IEnumerable<IdentityError> IdentityError { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }

        //Converts the object to Json incase of creating a response object as string
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

}
