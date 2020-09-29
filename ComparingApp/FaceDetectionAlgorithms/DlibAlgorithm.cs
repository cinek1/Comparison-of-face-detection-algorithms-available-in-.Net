using ComparingApp.Interfaces;
using FaceRecognitionDotNet;
using OpenCvSharp;
using System.Linq;

namespace ComparingApp.FaceDetectionAlgorithms
{
    class DlibAlgorithm : IDetectFace
    {
        private  FaceRecognition _faceRecognition;
        public DlibAlgorithm()
        {
            _faceRecognition = FaceRecognition.Create(@"Classifiers\dlib-models\");
        }

        public Mat Detect(Mat mat)
        {
            using (var unknownImage = FaceRecognition.LoadImage(OpenCvSharp.Extensions.BitmapConverter.ToBitmap(mat)))
            {
                var faceLocations = _faceRecognition.FaceLocations(unknownImage, 0, FaceRecognitionDotNet.Model.Hog).ToList();
                faceLocations.ForEach(r => mat.Rectangle(new OpenCvSharp.Rect(r.Left, r.Top, r.Right - r.Left, r.Bottom - r.Top), Scalar.Red)); 
            }
            return mat; 
        }
    }
}
