using ContentManagement.Domain.Commons;
using System;
using System.Collections.Generic;

namespace ContentManagement.Application.CustomExceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string msg) : base(msg)
        {

        }
        public BadRequestException(IList<Error> errors, string msg) : base(msg)
        {
            Errors = errors;
        }

        public IList<Error> Errors { get; set; }
    }
}
