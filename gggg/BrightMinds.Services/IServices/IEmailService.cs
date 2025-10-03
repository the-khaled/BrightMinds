﻿using BrightMinds.Services.Helpers.Emails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Services.IServices
{
    public interface IEmailService
    {

        public Task SendEmail(IEmailStructure emailStructure);


    }
}
