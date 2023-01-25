using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtraFunction.DTO_
{
    public class UpdateTermsAndConditionsDTO
    {
        public string newTitle { get; set; }
        public string newContent { get; set; }
        public string newUserRestrictions { get; set; }
        public string newRulesOfConduct { get; set; }
        public string newContactInformation { get; set; }
        public DateTime newDate { get; set; }
    }
}
