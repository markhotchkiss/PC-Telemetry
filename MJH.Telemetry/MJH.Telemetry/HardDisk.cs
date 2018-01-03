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
