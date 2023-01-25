using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtraFunction.Model
{
    public class AdminLogin
    {
        //[OpenApiExample(typeof(LoginExample))]
        [OpenApiProperty(Description = "Username for the Admin logging in.")]
        [JsonRequired]
        public string username { get; set; }

        [OpenApiProperty(Description = "Password for the Admin logging in.")]
        [JsonRequired]
        public string password { get; set; }

        //public class LoginExample : OpenApiExample<Login>
        //{
        //    public override IOpenApiExample<Login> Build(NamingStrategy NamingStrategy = null)
        //    {
        //        Examples.Add(OpenApiExampleResolver.Resolve("admin",
        //                                                    new Login()
        //                                                    {
        //                                                        username = "username",
        //                                                        password = "password"
        //                                                    },
        //                                                    NamingStrategy));

        //        return this;
        //    }
        //}
    }
}
