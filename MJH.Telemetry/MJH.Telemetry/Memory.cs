using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJH.Telemetry
{
    public class Memory : BaseTelemetry
    {
        public double TotalMemory { get; set; }

        public double UsedMemory => TotalMemory - RemainingMemory;

        public double RemainingMemory { get; set; }
    }
}
