﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ContentManagement.Infrastructure.Model
{
    public class AccountSettings
    {
        public string CloudName { get; set; }
        public string ApiSecret { get; set; }
        public string ApiKey { get; set; }
    }
}
