using ExtraFunction.DTO;
using ExtraFunction.Model;
using ExtraFunction.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtraFunction.Service
{
    public class AdminService : IAdminService
    {
        private IAdminRepository _adminRepository;
        public AdminService(IAdminRepository adminRepository)
        {
            this._adminRepository = adminRepository;
        }
        public async Task CreateAdmin(CreateAdminDTO admin)
        {
            await _adminRepository.CreateAdmin(admin);
        }
        public async Task UpdateAdminPassword(UpdateAdminDTO admin)
        {
            await _adminRepository.UpdateAdminPassword(admin);
        }
        public async Task<IEnumerable<GetAllAdminsDTO>> GetAllAdmins()
        {
            return await _adminRepository.GetAllAdmins();
        }

        public async Task<Admin> GetAdminById(Guid Id)
        {
            return await _adminRepository.GetAdminById(Id);
        }

    }
}
