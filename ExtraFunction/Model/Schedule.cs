using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtraFunction.Model
{
    public class Schedule
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [JsonRequired]
        [JsonProperty]
        public Guid UserId { get; set; }
        public List<DayOfWeek> DaysOfWeek { get; set; }
        [JsonRequired]
        public List<ScheduleTag> Tags { get; set; }
    }
}
