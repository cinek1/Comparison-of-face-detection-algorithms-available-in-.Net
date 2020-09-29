using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Autofac.Features.Indexed;
using ComparingApp.Interfaces;
using OpenCvSharp;

namespace ComparingApp.ViewModel
{
    class DlibViewModel : AlgorithmViewModel
    {
        private readonly IDetectFace _detectFace;
        public DlibViewModel(IIndex<Model.AlogrithmType, IDetectFace> viewers)
        {
            _detectFace = viewers[Model.AlogrithmType.Dlib];
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
