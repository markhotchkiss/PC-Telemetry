using System;
using System.Collections.Generic;

namespace MJH.Telemetry
{
    public class TelemetryFactory
    {
        public ITelemetryProvider<Telemetry<double>, Telemetry<Memory>, List<Telemetry<double>>, Telemetry<HardDisk>> GetProvider(InterfaceVersion version)
        {
            switch (version)
            {
                case InterfaceVersion.V1:
                    return new TelemetryProvider();
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}
