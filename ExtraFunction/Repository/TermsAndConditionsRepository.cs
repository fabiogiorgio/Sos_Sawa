using ExtraFunction.DAL;
using ExtraFunction.DTO_;
using ExtraFunction.Model;
using ExtraFunction.Repository_.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtraFunction.Repository_
{
    internal class TermsAndConditionsRepository : ITermsAndConditionRepository
    {
        private DatabaseContext _dbContext;

        public TermsAndConditionsRepository(DatabaseContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<TermsAndConditions> GetTermsAndConditions()
        {
            TermsAndConditions TermsAndConditions = _dbContext.TermsAndConditions.First();

            return TermsAndConditions;
        }
        public async Task UpdateTermsAndConditions(UpdateTermsAndConditionsDTO updateTermsAndConditionsDTO)                     
        {
            TermsAndConditions termsAndConditions = _dbContext.TermsAndConditions.First();

            termsAndConditions.Content = updateTermsAndConditionsDTO.newContent;
            termsAndConditions.ContactInformation = updateTermsAndConditionsDTO.newContactInformation;
            termsAndConditions.RulesOfConduct = updateTermsAndConditionsDTO.newRulesOfConduct;
            termsAndConditions.Title = updateTermsAndConditionsDTO.newTitle;
            termsAndConditions.UserRestrictions = updateTermsAndConditionsDTO.newUserRestrictions;
            termsAndConditions.Date = updateTermsAndConditionsDTO.newDate;

            _dbContext.Update(termsAndConditions);
            await _dbContext.SaveChangesAsync();

        }
    }
}
