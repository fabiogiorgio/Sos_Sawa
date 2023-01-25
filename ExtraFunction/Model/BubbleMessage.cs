using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtraFunction.Model
{
    public class BubbleMessage
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [JsonRequired]
        public string Message { get; set; }
    }
}
