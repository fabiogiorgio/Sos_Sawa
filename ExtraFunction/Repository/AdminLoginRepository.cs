using ExtraFunction.DAL;
using ExtraFunction.DTO;
using ExtraFunction.Model;
using ExtraFunction.Repository.Interface;
using ExtraFunction.Utils;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtraFunction.Repository
{
    public class AdminLoginRepository : IAdminLoginRepository
    {
        private DatabaseContext dbContext;

        public AdminLoginRepository(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<ReturnAdminLogin> CheckIfCredentialsCorrect(string userName, string password)
        {
            await dbContext.SaveChangesAsync();
            Admin admin = dbContext.Admins.FirstOrDefault(u => u.username.ToLower() == userName.ToLower());

            if (admin.IsNullOrDefault())
            {
                return new ReturnAdminLogin { id = Guid.Empty, ok = false};
            }
            else if (!admin.password.Equals(PasswordHasher.HashPassword(password)))
            {
                return new ReturnAdminLogin { id = Guid.Empty, ok = false };
            }
            else
            {
                return new ReturnAdminLogin { id = admin.Id, ok = true };
            }

        }
    }
}
