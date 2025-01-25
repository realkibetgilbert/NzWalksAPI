using Serilog;

namespace NzWalks.API.Extensions
{
    public static class LoggerSetupExtension
    {
        public static WebApplicationBuilder ConfigureSerilogLogger(this WebApplicationBuilder builder)
        {
            builder.Host.UseSerilog();

            Log.Logger = new
                LoggerConfiguration().WriteTo.File("C:\\Temp\\NZWALKS.Logs-.log",
                rollingInterval: RollingInterval.Day)

                .MinimumLevel.Warning()
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", Serilog.Events.LogEventLevel.Warning)

                .CreateLogger();

            return builder;
        }
    }
}
