using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Text;

namespace ITI.SkyLord.Tests
{
    [TestFixture]
    public class SetupAndTearDown
    {
        [SetUp]
        public void SetUp()
        {
            string connectionName = "ITI.SkyLord";
            string dllConfigName = "ITI.SkyLord.Tests.dll.config";
            ProcessStartInfo processStartInfo = new ProcessStartInfo
            {
                FileName = "migrate.exe",
                Arguments = "ITI.SkyLord.dll ITI.SkyLord.Migrations.Configuration /connectionStringName=" + connectionName + " /StartUpConfigurationFile=\"" + dllConfigName + "\"",
                UseShellExecute = false
            };
            using (Process p = Process.Start(processStartInfo))
            {
                p.WaitForExit();
            }
        }

        [TearDown]
        public void TearDown()
        {
            Console.WriteLine("C'est terminé.");
        }
    }
}
