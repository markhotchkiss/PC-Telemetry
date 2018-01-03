namespace MJH.Telemetry
{
    public class HardDisk
    {
        public double FreeSpace { get; set; }

        public double UsedSpace => TotalSpace - FreeSpace;

        public double TotalSpace { get; set; }

        public string DriveLetter { get; set; }
    }
}
