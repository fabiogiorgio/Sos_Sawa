using ExtraFunction.DTO;
using ExtraFunction.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtraFunction.Service
{
    public class AdminLoginService : IAdminLoginService
    {
        private IAdminLoginRepository _adminLoginRepository;
        public AdminLoginService(IAdminLoginRepository loginRepository)
        {
            this._adminLoginRepository = loginRepository;
        }
        public async Task<ReturnAdminLogin> CheckIfCredentialsCorrect(string userName, string password)
        {
            ReturnAdminLogin result = await _adminLoginRepository.CheckIfCredentialsCorrect(userName, password);

            return result;
        }

    }
}
