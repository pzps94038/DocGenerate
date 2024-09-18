using DocGenerate.DatabaseContext;
using DocGenerate.Forms;
using DocGenerate.Helper;
using DocGenerate.Helper.APIDoc;
using DocGenerate.Helper.SqlExcelDoc;
using DocGenerate.Interface.APIDoc;
using DocGenerate.Interface.SqlExcelDoc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System;

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
                Application.ThreadException += Application_ThreadException;
                AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
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
                services.AddTransient<DbDocGenerateForm>();
                services.AddTransient<AddDbSettingForm>();
                services.AddTransient<SelfSignedCertificateForm>();
                services.AddTransient<APIDocForm>();
                services.AddSingleton<IWebViewHelper, WebViewHelper>();
                services.AddSingleton<ISharedHelper, SharedHelper>();
                services.AddSingleton<ILogHelper, LogHelper>();
                services.AddSingleton<ISqlExcelDocHelper, SqlExcelDocHelper>();
                services.AddDbContext<DocGenerateDbContext>();
                _serviceProvider = services.BuildServiceProvider();
                var db = _serviceProvider.GetRequiredService<DocGenerateDbContext>();
                db.Database.EnsureCreated();
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

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            try
            {
                var sharedHelper = _serviceProvider?.GetRequiredService<ISharedHelper>();
                sharedHelper?.ShowExceptionMessageBox(e.Exception);
            }
            catch (Exception)
            {

            }
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                var sharedHelper = _serviceProvider?.GetRequiredService<ISharedHelper>();
                var ex = (Exception)e.ExceptionObject;
                sharedHelper?.ShowExceptionMessageBox(ex);
            }
            catch (Exception)
            {

            }
        }
    }
}