using Autofac.Features.Indexed;
using ComparingApp.Interfaces;
using OpenCvSharp;
using System.Drawing;

namespace ComparingApp.ViewModel
{
   public class HaarCascadeViewModel : AlgorithmViewModel
    {
        private readonly IDetectFace _detectFace;
        public HaarCascadeViewModel(IIndex<Model.AlogrithmType, IDetectFace> viewers)
        {
            _detectFace = viewers[Model.AlogrithmType.HaarCascade];
        }
        public override void Update(Mat mat)
        {
            _detectFace.Detect(mat);
            Bitmap bitmapImage = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(mat);
            ImageViewer = base.ConvertToBitmatImage(bitmapImage);
            mat.Dispose(); 
        }
    }
}
