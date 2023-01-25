using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtraFunction.DTO
{
    public class UpdateAdminDTO
    {
        public Guid adminId { get; set; }
        public string newPassword { get; set; }
    }
}
