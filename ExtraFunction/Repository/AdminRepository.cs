using AutoMapper;
using ExtraFunction.DAL;
using ExtraFunction.DTO;
using ExtraFunction.Model;
using ExtraFunction.Repository.Interface;
using ExtraFunction.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtraFunction.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private DatabaseContext _dbContext;

        public AdminRepository(DatabaseContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task CreateAdmin(CreateAdminDTO admin)
        {
            Mapper mapper = AutoMapperUtil.ReturnMapper(new MapperConfiguration(con => con.CreateMap<CreateAdminDTO, Admin>()));
            Admin fullAdmin = mapper.Map<Admin>(admin);
            fullAdmin.password = PasswordHasher.HashPassword(fullAdmin.password);

            _dbContext.Admins?.Add(fullAdmin);


            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateAdminPassword(UpdateAdminDTO updateAdminDTO)
        {
            Admin admin = await _dbContext.Admins.FirstOrDefaultAsync(a => a.Id == updateAdminDTO.adminId);
                admin.password = PasswordHasher.HashPassword(updateAdminDTO.newPassword);
            _dbContext.Admins.Update(admin);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<Admin> GetAdminById(Guid id)
        {
            await _dbContext.SaveChangesAsync();
            Admin admin = _dbContext.Admins.FirstOrDefault(a => a.Id == id);
            return admin;
        }
        public async Task<IEnumerable<GetAllAdminsDTO>> GetAllAdmins()
        {
            List<Admin> admins = await _dbContext.Admins.ToListAsync();

            Mapper mapper = AutoMapperUtil.ReturnMapper(new MapperConfiguration(con => con.CreateMap<Admin, GetAllAdminsDTO>()));
            List<GetAllAdminsDTO> getAllAdmins = new List<GetAllAdminsDTO>();

            admins.ForEach(delegate (Admin admin)
            {
                getAllAdmins.Add(mapper.Map<GetAllAdminsDTO>(admin));
            });
            return getAllAdmins;
        }
    }
}
