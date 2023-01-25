using ExtraFunction.DTO;
using ExtraFunction.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtraFunction.Repository.Interface
{
    public interface IAdminService
    {
        public Task CreateAdmin(CreateAdminDTO admin);
        public Task UpdateAdminPassword(UpdateAdminDTO admin);
        public Task<Admin> GetAdminById(Guid id);
        public Task<IEnumerable<GetAllAdminsDTO>> GetAllAdmins();
    }
}
