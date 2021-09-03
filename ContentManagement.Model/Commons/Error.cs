using System;
using System.Collections.Generic;
using System.Text;

namespace ContentManagement.Domain.Commons
{
    public class Error
    {
        public string PropertyName { get; set; }
        public string PropertyValue { get; set; }
        public bool HasValidationErrors { get; set; }
    }
}
