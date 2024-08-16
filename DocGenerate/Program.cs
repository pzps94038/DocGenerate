using DocGenerate.Helper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace DocGenerate
{
    internal static class Program
    {
        private static ServiceProvider? _serviceProvider;
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.ApplicationExit += new EventHandler(OnApplicationExit!);
                Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
                // To customize application configuration such as set high DPI settings or default font,
                // see https://aka.ms/applicationconfiguration.
                ApplicationConfiguration.Initialize();
                //使用相依性插入以啟用各項服務
                ServiceCollection services = new();
                // Log
                services.AddLogging(loggingBuilder =>
                {
                    loggingBuilder.ClearProviders();
                    loggingBuilder.AddSerilog();
                });
                services.AddSingleton<ChooseDocGenerateForm>();
                services.AddSingleton<DbDocGenerateForm>();
                services.AddSingleton<ISharedHelper, SharedHelper>();
                services.AddSingleton<ILogHelper, LogHelper>();
                _serviceProvider = services.BuildServiceProvider();
                var form = _serviceProvider.GetRequiredService<ChooseDocGenerateForm>();
                form.StartPosition = FormStartPosition.CenterScreen;
                var logHelper = _serviceProvider.GetRequiredService<ILogHelper>();
                logHelper.Info("Program Start.");
                form.StartPosition = FormStartPosition.CenterScreen;
                Application.Run(form);
            }
            catch (Exception ex)
            {
                var helper = _serviceProvider?.GetRequiredService<ISharedHelper>();
                helper?.ShowExceptionMessageBox(ex);
                // 關閉應用程式
                Application.Exit();
            }
        }

        private static void OnApplicationExit(object sender, EventArgs e)
        {
            var logHelper = _serviceProvider?.GetRequiredService<ILogHelper>();
            logHelper?.Info("Program End.");
        }
    }
}