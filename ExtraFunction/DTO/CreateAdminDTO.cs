using Newtonsoft.Json;
using System.Collections.Generic;
using System;

namespace ExtraFunction.DTO
{
    public class CreateAdminDTO
    {
        [JsonRequired]
        public string username { get; set; }
        [JsonRequired]
        public string password { get; set; }
    }
}
