using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtraFunction.Model
{
    public class Admin
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string username { get; set; }
        public string password { get; set; }
    }
}
