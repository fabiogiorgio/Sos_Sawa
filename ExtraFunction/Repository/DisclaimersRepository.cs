using ExtraFunction.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtraFunction.Model;
using ExtraFunction.Repository_.Interface;
using ExtraFunction.DTO_;
using Microsoft.EntityFrameworkCore;

namespace ExtraFunction.Repository_
{
    public class DisclaimersRepository : IDisclaimersRepository
    {
        private DatabaseContext _dbContext;

        public DisclaimersRepository(DatabaseContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<Disclaimers> GetDisclaimers()
        {         
            Disclaimers disclaimers = _dbContext.Disclaimers.First();
            return disclaimers;
        }
        public async Task UpdateDisclaimers(UpdateDisclaimersDTO updateDisclaimerDTO)
        {
            Disclaimers disclaimers = _dbContext.Disclaimers.First();

            disclaimers.disclaimers = updateDisclaimerDTO.newDisclaimer;
            _dbContext.Update(disclaimers);
            await _dbContext.SaveChangesAsync();

        }
    }
}
