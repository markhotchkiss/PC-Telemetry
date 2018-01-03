using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Management;

namespace MJH.Telemetry
{
    public class TelemetryProvider : ITelemetryProvider<Telemetry<double>, Telemetry<Memory>, List<Telemetry<double>>, List<Telemetry<HardDisk>>>
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

        Telemetry<Memory> ITelemetryProvider<Telemetry<double>, Telemetry<Memory>, List<Telemetry<double>>, List<Telemetry<HardDisk>>>.Memory()
        {
            var ramr = new PerformanceCounter("Memory", "Available MBytes");

            var totalMemory = GetRamSize();

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

        List<Telemetry<double>> ITelemetryProvider<Telemetry<double>, Telemetry<Memory>, List<Telemetry<double>>, List<Telemetry<HardDisk>>>.Network()
        {
            var networkCards = GetNetworkCards();

            var telemetryData = new List<Telemetry<double>>();

            foreach (var networkInterface in networkCards)
            {
                var utilization = GetNetworkUtilization(networkInterface);

                var telemetry = new Telemetry<double>()
                {
                    Data = Convert.ToDouble(utilization),
                    DateTime = DateTime.Now,
                    Message = "Successfully checked utilization."
                };

                telemetryData.Add(telemetry);
            }

            return telemetryData;
        }

        List<Telemetry<HardDisk>> ITelemetryProvider<Telemetry<double>, Telemetry<Memory>, List<Telemetry<double>>, List<Telemetry<HardDisk>>>.HardDiskSpace()
        {
            var drives = DriveInfo.GetDrives();

            var driveList = new List<Telemetry<HardDisk>>();

            foreach (var drive in drives)
            {
                var availableSpace = GetTotalFreeSpace(drive.Name);
                var totalSize = GetTotalSize(drive.Name);

                var d = new Telemetry<HardDisk>
                {
                    Data = new HardDisk()
                    {
                        TotalSpace = totalSize,
                        FreeSpace = availableSpace,
                        DriveLetter = drive.Name

                    }
                };
                driveList.Add(d);
            }

            return driveList;
        }

        private static string GetRamSize()
        {
            var mc = new ManagementClass("Win32_ComputerSystem");
            var moc = mc.GetInstances();
            foreach (var item in moc)
            {
                return Convert.ToString(Math.Round(Convert.ToDouble(item.Properties["TotalPhysicalMemory"].Value) / 1048576, 2));
            }

            return "RAMsize";
        }

        public static double GetNetworkUtilization(string networkCard)
        {
            const int numberOfIterations = 10;

            var bandwidthCounter = new PerformanceCounter("Network Interface", "Current Bandwidth", networkCard);

            var bandwidth = bandwidthCounter.NextValue();

            var dataSentCounter = new PerformanceCounter("Network Interface", "Bytes Sent/sec", networkCard);

            var dataReceivedCounter = new PerformanceCounter("Network Interface", "Bytes Received/sec", networkCard);

            float sendSum = 0;

            float receiveSum = 0;

            for (var index = 0; index < numberOfIterations; index++)
            {
                sendSum += dataSentCounter.NextValue();

                receiveSum += dataReceivedCounter.NextValue();
            }

            var dataSent = sendSum;

            var dataReceived = receiveSum;

            var utilization = (8 * (dataSent + dataReceived)) / (bandwidth * numberOfIterations) * 100;

            return utilization;
        }

        public string[] GetNetworkCards()
        {
            var category = new PerformanceCounterCategory("Network Interface");
            var instancenames = category.GetInstanceNames();

            return instancenames;
        }

        private double GetTotalFreeSpace(string driveName)
        {
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady && drive.Name == driveName)
                {
                    return Math.Round(Convert.ToDouble(drive.AvailableFreeSpace / 1073741824.0), 2);
                }
            }
            return 0.0;
        }

        private double GetTotalSize(string driveName)
        {
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady && drive.Name == driveName)
                {
                    return Math.Round(Convert.ToDouble(drive.TotalSize / 1073741824.0), 2);
                }
            }
            return 0.0;
        }
    }
}