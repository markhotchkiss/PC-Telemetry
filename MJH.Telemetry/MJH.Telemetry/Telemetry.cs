using System;

namespace MJH.Telemetry
{
    public class Telemetry<T> : BaseTelemetry
    {
        public T Data { get; set; }
    }
}
