using ExtraFunction.DTO_;
using ExtraFunction.Model;
using ExtraFunction.Repository_;
using ExtraFunction.Repository_.Interface;
using System.Threading.Tasks;

namespace ExtraFunction.Service
{
    public class TermsAndConditionsService : ITermsAndConditionsService
    {
        private ITermsAndConditionRepository _termsAndConditionRepository;
        public TermsAndConditionsService(ITermsAndConditionRepository termsAndConditionRepository)
        {
            this._termsAndConditionRepository = termsAndConditionRepository;
        }
        public Task<TermsAndConditions> GetTermsAndConditions()
        {
            return _termsAndConditionRepository.GetTermsAndConditions();
        }
        public async Task UpdateTermsAndConditions(UpdateTermsAndConditionsDTO updateTermsAndConditionsDTO)
        {
            await _termsAndConditionRepository.UpdateTermsAndConditions(updateTermsAndConditionsDTO);
        }
    }
}
