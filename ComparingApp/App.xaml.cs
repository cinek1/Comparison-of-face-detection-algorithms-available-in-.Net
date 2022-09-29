using Autofac;
using ComparingApp.FaceDetectionAlgorithms;
using ComparingApp.Interfaces;
using System.Windows;
using ComparingApp.ViewModel;
using ComparingApp.Utils;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using ComparingApp.Extensions;

namespace ComparingApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IServiceCollection services;
        public IConfigurationRoot Configuration { get; private set; }

        public App() : base()
        {
            services = new ServiceCollection();
        }
        private void OnStartup(object sender, StartupEventArgs e)
        {
            RegisterServices(services, BuildConfiguration());
            var srvProvider = services.BuildServiceProvider(); 
            var mainWindow = srvProvider.GetRequiredService<MainWindow>();
            var cameraCapture = srvProvider.GetRequiredService<CameraCapture>();
            mainWindow.Show();
            cameraCapture.OpenCamera();
            Task.Run(() => cameraCapture.DetectPresence());
        }

        private IConfiguration BuildConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build(); 
        }

        private IServiceCollection RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            return services.AddFaceDetectionAlgorithms()
                           .AddSingleton<AlgorithmViewModelFactory>()
                           .AddSingleton<MainWindow>()
                           .AddSingleton<CameraCapture>()
                           .AddSingleton(configuration);
        }
    }
}
