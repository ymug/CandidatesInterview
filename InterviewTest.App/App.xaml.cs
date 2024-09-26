using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using InterviewTest.App.RemoteWorkerSimulator_DO_NOT_TOUCH;
using InterviewTest.App.Services;
using Microsoft.Extensions.DependencyInjection;

namespace InterviewTest.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;


        public App()
        {
            _serviceProvider = ConfigureServices();
        }


        #region DO NOT REMOVE

        private CancellationTokenSource _cancellationTokenSource;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            _cancellationTokenSource = new CancellationTokenSource();
            new RemoteProductWorkerSimulator(_serviceProvider.GetRequiredService<IProductStore>()).Run(_cancellationTokenSource.Token);

            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            _cancellationTokenSource.Cancel();
        }

        #endregion


        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IProductStore, ProductStore>();


            services.AddSingleton<MainWindow>();

            return services.BuildServiceProvider();
        }

    }
}
