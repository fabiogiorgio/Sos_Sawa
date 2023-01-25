using ExtraFunction.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ExtraFunction.Repository_.Interface
{
    public interface ITokenService
    {
        Task<LoginResult> CreateToken(Login Login);
        Task<LoginResult> CreateAdminToken(AdminLogin Login);

        Task<ClaimsPrincipal> GetByValue(string Value);
    }

}
