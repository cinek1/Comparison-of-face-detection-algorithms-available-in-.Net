using Autofac.Features.Indexed;
using ComparingApp.Interfaces;
using OpenCvSharp;
using System.Drawing;

namespace ComparingApp.ViewModel
{
    class DnnViewModel : AlgorithmViewModel
    {
        private readonly IDetectFace _detectFace;
        public DnnViewModel(IIndex<Model.AlogrithmType, IDetectFace> viewers)
        {
            _detectFace = viewers[Model.AlogrithmType.Dnn];
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
