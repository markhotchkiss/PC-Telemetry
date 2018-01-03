using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJH.Telemetry
{
    public class HardDisk
    {
        public double FreeSpace => TotalSpace - UsedSpace;

        public double UsedSpace { get; set; }

        public double TotalSpace { get; set; }

        public string DriveLetter { get; set; }
    }
}
