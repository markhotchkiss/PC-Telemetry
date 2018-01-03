using System;
using System.Diagnostics;
using System.Management;

namespace MJH.Telemetry
{
    public class TelemetryProvider : ITelemetryProvider<Telemetry<double>, Telemetry<Memory>, double, Telemetry<HardDisk>>
    {
        public Telemetry<double> Cpu()
        {
            var processor = new ManagementObject("Win32_PerfFormattedData_PerfOS_Processor.Name='_Total'");
            processor.Get();

            return new Telemetry<double>
            {
                DateTime = DateTime.Now,
                Message = "Calculated value correctly.",
                Data = double.Parse(processor.Properties["PercentProcessorTime"].Value.ToString())
            };
        }

        Telemetry<Memory> ITelemetryProvider<Telemetry<double>, Telemetry<Memory>, double, Telemetry<HardDisk>>.Memory()
        {
            var ramr = new PerformanceCounter("Memory", "Available MBytes");

            var totalMemory = getRAMsize();

            return new Telemetry<Memory>
            {
                DateTime = DateTime.Now,
                Message = "Calculated value correctly.",
                Data = new Memory()
                {
                    TotalMemory = Convert.ToDouble(totalMemory),
                    RemainingMemory = ramr.NextValue(),
                    DateTime = DateTime.Now
                } 
            };
        }

        double ITelemetryProvider<Telemetry<double>, Telemetry<Memory>, double, Telemetry<HardDisk>>.Network()
        {
            throw new NotImplementedException();
        }

        Telemetry<HardDisk> ITelemetryProvider<Telemetry<double>, Telemetry<Memory>, double, Telemetry<HardDisk>>.HardDiskSpace()
        {
            throw new NotImplementedException();
        }

        private static String getRAMsize()
        {
            ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject item in moc)
            {
                return Convert.ToString(Math.Round(Convert.ToDouble(item.Properties["TotalPhysicalMemory"].Value) / 1048576, 2));
            }

            return "RAMsize";
        }
    }
}