using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJH.Telemetry
{
    public interface ITelemetryProvider<out TCpu, out TMemory, out TNetwork, out THardDisk>
    {
        TCpu Cpu();

        TMemory Memory();

        TNetwork Network();

        THardDisk HardDiskSpace();
    }
}