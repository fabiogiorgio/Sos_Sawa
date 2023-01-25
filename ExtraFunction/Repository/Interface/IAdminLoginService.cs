using ExtraFunction.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtraFunction.Repository.Interface
{
    public interface IAdminLoginService
    {
        Task<ReturnAdminLogin> CheckIfCredentialsCorrect(string userName, string password);
    }
}
