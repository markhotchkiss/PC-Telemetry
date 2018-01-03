namespace MJH.Telemetry
{
    public class Memory : BaseTelemetry
    {
        public double TotalMemory { get; set; }

        public double UsedMemory => TotalMemory - RemainingMemory;

        public double RemainingMemory { get; set; }
    }
}
