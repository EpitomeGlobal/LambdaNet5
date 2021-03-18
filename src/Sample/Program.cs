using System;
using System;
using System.IO;
using Amazon.XRay.Recorder.Core;
using Audit.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;
	
namespace Sample
{
    class Program
    {
		/// <summary>
        ///     Gets or sets configuration root.
        /// </summary>
        public static IConfigurationRoot? Configuration { get; set; }

        /// <summary>
        ///     Default set up.
        /// </summary>
        public static void SetUp()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();
            AWSXRayRecorder.InitializeInstance(Configuration);
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();
            Audit.Core.Configuration.Setup().UseSerilog();
            Audit.Core.Configuration.AuditDisabled = true;
        }
		
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
