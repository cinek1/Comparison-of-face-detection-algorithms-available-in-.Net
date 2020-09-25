using Autofac.Features.Indexed;
using ComparingApp.Interfaces;
using ComparingApp.Utils;
using System.Windows;


namespace ComparingApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(IIndex<Model.AlogrithmType, AlgorithmViewModel> algorithmsViewModels)
        {
            InitializeComponent();
            Image_HaarCascade.DataContext = algorithmsViewModels[Model.AlogrithmType.HaarCascade];
            Image_UltraFace.DataContext = algorithmsViewModels[Model.AlogrithmType.UltraFace];
            Image_Dnn.DataContext = algorithmsViewModels[Model.AlogrithmType.Dnn]; 
        }
    }
}
