using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtraFunction.Model
{
    public class ScheduleTag
    {
        [JsonRequired]
        public string Name { get; set; }

        [JsonRequired]
        public double ActivityDuration { get; set; }

        [JsonRequired]
        public WaterTemperature waterTemperature { get; set; }

        [JsonRequired]
        public bool IsWaterOn { get; set; }


    }
}
