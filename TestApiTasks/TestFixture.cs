using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApiTasks
{
    class TestFixture<TStartup> : IDisposable where TStartup : class
    {
      public readonly TestServer Server;
    public TestFixture()
    {
            var builder = new WebHostBuilder()
          .UseContentRoot(Directory.GetCurrentDirectory())
          .UseConfiguration(new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json")
          .Build()).UseStartup<TStartup>();

            Server = new TestServer(builder);
        }
    public void Dispose()
    {
        Server.Dispose();
    }
    }
}
