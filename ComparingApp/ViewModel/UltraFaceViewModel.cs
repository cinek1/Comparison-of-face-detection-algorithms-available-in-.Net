using Autofac.Features.Indexed;
using ComparingApp.Interfaces;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ComparingApp.ViewModel
{
    public class UltraFaceViewModel : AlgorithmViewModel
    {
        private readonly IDetectFace _detectFace;
        public UltraFaceViewModel(IIndex<Model.AlogrithmType, IDetectFace> viewers)
        {
            _detectFace = viewers[Model.AlogrithmType.UltraFace];
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
