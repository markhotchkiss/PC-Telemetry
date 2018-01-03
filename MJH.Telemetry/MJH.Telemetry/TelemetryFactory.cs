using System;

namespace MJH.Telemetry
{
    public class TelemetryFactory
    {
        public ITelemetryProvider<Telemetry<double>, Telemetry<Memory>, double, Telemetry<HardDisk>> GetProvider(InterfaceVersion version)
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
