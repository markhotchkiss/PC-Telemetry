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