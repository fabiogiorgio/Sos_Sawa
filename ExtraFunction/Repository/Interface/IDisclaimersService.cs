﻿using ExtraFunction.DTO_;
using ExtraFunction.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtraFunction.Repository_.Interface
{
    public interface IDisclaimersService
    {
        public Task<Disclaimers> GetDisclaimers();
        public Task UpdateDisclaimers(UpdateDisclaimersDTO updateDisclaimerDTO);
    }
}
