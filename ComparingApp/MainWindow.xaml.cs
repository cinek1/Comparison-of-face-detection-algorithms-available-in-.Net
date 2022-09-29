using Autofac.Features.Indexed;
using ComparingApp.Interfaces;
using ComparingApp.Utils;
using System.Windows;
using System.Linq;
using System.Collections.Generic;
using ComparingApp.ViewModel;

namespace ComparingApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(IEnumerable<IDetectFace> algorithms, AlgorithmViewModelFactory factory)
        {
            InitializeComponent();
            InitialiazeAlgorithms(algorithms, factory); 
        }

        private void InitialiazeAlgorithms(IEnumerable<IDetectFace> algorithms, AlgorithmViewModelFactory factory)
        {
            var algorithmsDict = algorithms.ToDictionary(w => w.AlogrithmType, w => w);
            Image_HaarCascade.DataContext = factory.Create(algorithmsDict[Model.AlogrithmType.HaarCascade]);
            Image_UltraFace.DataContext = factory.Create(algorithmsDict[Model.AlogrithmType.UltraFace]);
            Image_Dnn.DataContext = factory.Create(algorithmsDict[Model.AlogrithmType.Dnn]);
            Image_Dlib.DataContext = factory.Create(algorithmsDict[Model.AlogrithmType.Dlib]);
        }
    }
}
