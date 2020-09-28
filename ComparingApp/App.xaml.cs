using Autofac;
using ComparingApp.FaceDetectionAlgorithms;
using ComparingApp.Interfaces;
using System.Windows;
using ComparingApp.ViewModel;
using ComparingApp.Utils;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ComparingApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IContainer container;
        public IConfigurationRoot Configuration { get; private set; }

        public App() : base()
        {
            BuildConfiguration(); 
            var builder = new ContainerBuilder();
            builder.RegisterType<HaarCascadeFaceDetector>().Keyed<IDetectFace>(Model.AlogrithmType.HaarCascade)
                .WithParameter(new TypedParameter(typeof(IConfiguration), Configuration));
            builder.RegisterType<UltraFaceDetection>().Keyed<IDetectFace>(Model.AlogrithmType.UltraFace)
                .WithParameter(new TypedParameter(typeof(IConfiguration), Configuration));
            builder.RegisterType<DnnFaceDetector>().Keyed<IDetectFace>(Model.AlogrithmType.Dnn)
                .WithParameter(new TypedParameter(typeof(IConfiguration), Configuration));
                
            builder.RegisterType<UltraFaceViewModel>().Keyed<AlgorithmViewModel>(Model.AlogrithmType.UltraFace)
                .SingleInstance();
            builder.RegisterType<HaarCascadeViewModel>().Keyed<AlgorithmViewModel>(Model.AlogrithmType.HaarCascade)
                .SingleInstance();
            builder.RegisterType<DnnViewModel>().Keyed<AlgorithmViewModel>(Model.AlogrithmType.Dnn)
                .SingleInstance();
            builder.RegisterType<MainWindow>().SingleInstance();
            builder.RegisterType<CameraCapture>().SingleInstance()
                .WithParameter(new TypedParameter(typeof(IConfiguration), Configuration)); 
            
            container = builder.Build(); 
        }
        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = container.Resolve<MainWindow>();
            mainWindow.Show();
            var cameraCapture = container.Resolve<CameraCapture>();
            cameraCapture.OpenCamera();
            Task.Run(() => cameraCapture.DetectPresence());
        }

        private void BuildConfiguration()
        {
            var conf = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            Configuration = conf.Build(); 
        }
        
    }
}
