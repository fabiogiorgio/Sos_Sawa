using ExtraFunction.DTO_;
using ExtraFunction.Model;
using ExtraFunction.Repository_.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ExtraFunction.Service
{
    public class DisclaimersService : IDisclaimersService
    {
        private IDisclaimersRepository _disclaimerRepository;

        public DisclaimersService(IDisclaimersRepository disclaimerRepository)
        {
            this._disclaimerRepository = disclaimerRepository;
        }
        public Task<Disclaimers> GetDisclaimers()
        {
            return _disclaimerRepository.GetDisclaimers();
        }
        public async Task UpdateDisclaimers(UpdateDisclaimersDTO updateDisclaimerDTO)
        {
            await _disclaimerRepository.UpdateDisclaimers(updateDisclaimerDTO);
        }



    }
}
