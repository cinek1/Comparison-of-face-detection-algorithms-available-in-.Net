using ComparingApp.Interfaces;
using OpenCvSharp;
using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using ComparingApp.Model;

namespace ComparingApp.FaceDetectionAlgorithms
{
    class HaarCascadeFaceDetector : IDetectFace
    {
        private readonly int minimumNeighbors;
        private readonly decimal scaleFactor;
        private readonly int minSizeWidth;
        private readonly int minSizeHeight;
        private readonly CascadeClassifier cascadeClassifier;

        public HaarCascadeFaceDetector(IConfiguration configuration)
        {
            int.TryParse(configuration["Algos:HarrCascade:MinimumNeighbors"], out minimumNeighbors);
            decimal.TryParse(configuration["Algos:HarrCascade:ScaleFactor"], out scaleFactor);
            int.TryParse(configuration["Algos:HarrCascade:MinSizeWidth"], out minSizeWidth);
            int.TryParse(configuration["Algos:HarrCascade:MinSizeHeight"], out minSizeHeight);
            cascadeClassifier = new CascadeClassifier(configuration["Algos:HarrCascade:CascadeClassifier"]);
        }

        public AlogrithmType AlogrithmType => AlogrithmType.HaarCascade;

        public Mat Detect(Mat mat)
        {
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
