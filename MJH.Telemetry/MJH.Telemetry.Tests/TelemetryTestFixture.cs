using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MJH.Telemetry.Tests
{
    [TestFixture]
    public class TelemetryTestFixture
    {
        private ITelemetryProvider<Telemetry<double>, Telemetry<Memory>, List<Telemetry<double>>, List<Telemetry<HardDisk>>> _telemetry;



        [OneTimeSetUp]
        public void SetupTests()
        {
            _telemetry = new TelemetryFactory().GetProvider(InterfaceVersion.V1);
        }

        [Test]
        public void GetCpuUtilization()
        {
            for (int i = 0; i < 1000; i++)
            {
                var result = _telemetry.Cpu();

                Console.WriteLine($"Value: {result.Data}");

                Assert.Greater(result.Data, -1);
                Assert.IsNotNull(result.Data);
            }
        }

        [Test]
        public void GetMemoryUtilization()
        {
            var result = _telemetry.Memory();

            Console.WriteLine($"Value: {result.Data}");

            Assert.Greater(result.Data.RemainingMemory, 0);
            Assert.AreEqual(result.Data.UsedMemory + result.Data.RemainingMemory, result.Data.TotalMemory);
        }

        [Test]
        public void GetNetworkUtilization()
        {
            var result = _telemetry.Network();

            Assert.NotNull(result.First());
            Assert.GreaterOrEqual(result.First().DateTime, DateTime.Now);
        }

        [Test]
        public void GetHardDiskUtilization()
        {
            var result = _telemetry.HardDiskSpace();

            Assert.Greater(result.Count, 0);
            Assert.IsNotNull(result);
        }
    }
}
