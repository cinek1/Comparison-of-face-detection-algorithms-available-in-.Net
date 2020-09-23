using ComparingApp.Interfaces;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Linq; 
namespace ComparingApp.FaceDetectionAlgorithms
{
    class HaarCascadeFaceDetector : IDetectFace
    {

        private readonly string classifierPath = "Classifiers/haarcascade_frontalface_default.xml"; 
        public Mat Detect(Mat mat)
        {
            var minimumNeighbors = 5;
            var scaleFactor = 1.3M;
            var minSizeWidth = 50;
            var minSizeHeight = 50;
            var cascadeClassifier = new CascadeClassifier(classifierPath);
            using (var gray = new Mat())
            {
                Cv2.CvtColor(InputArray.Create(mat), OutputArray.Create(gray), ColorConversionCodes.BGR2GRAY);
                cascadeClassifier.DetectMultiScale(gray, Convert.ToDouble(scaleFactor), minimumNeighbors, 0,
                   new OpenCvSharp.Size(minSizeWidth, minSizeHeight))
               .ToList().ForEach(r => mat.Rectangle(new OpenCvSharp.Rect(r.X, r.Y, r.Width, r.Height), Scalar.Red));
                return mat; 
            }
        }
    }
}
