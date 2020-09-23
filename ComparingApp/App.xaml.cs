using Autofac;
using ComparingApp.FaceDetectionAlgorithms;
using ComparingApp.Interfaces;
using System.Windows;
using ComparingApp.ViewModel;
using ComparingApp.Utils;
using System.Threading.Tasks;

namespace ComparingApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IContainer container; 
        public App() : base()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<HaarCascadeFaceDetector>().Keyed<IDetectFace>(Model.AlogrithmType.HaarCascade);
            builder.RegisterType<UltraFaceDetection>().Keyed<IDetectFace>(Model.AlogrithmType.UltraFace);
            builder.RegisterType<UltraFaceViewModel>().Keyed<AlgorithmViewModel>(Model.AlogrithmType.UltraFace).SingleInstance();
            builder.RegisterType<HaarCascadeViewModel>().Keyed<AlgorithmViewModel>(Model.AlogrithmType.HaarCascade).SingleInstance();
            builder.RegisterType<MainWindow>().SingleInstance();
            builder.RegisterType<CameraCapture>().SingleInstance(); 
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
        
    }
}
