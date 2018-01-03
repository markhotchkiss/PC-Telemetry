using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MJH.Telemetry.Tests
{
    [TestFixture]
    public class TelemetryTestFixture
    {
        private ITelemetryProvider<Telemetry<double>, Telemetry<Memory>, double, Telemetry<HardDisk>> _telemetry;



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

        }

        [Test]
        public void GetHardDiskUtilization()
        {

        }
    }
}
