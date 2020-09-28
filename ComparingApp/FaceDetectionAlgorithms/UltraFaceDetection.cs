using ComparingApp.Interfaces;
using OpenCvSharp;
using System.Linq;
using System.Collections.Generic;
using UltraFaceDotNet;
using Microsoft.Extensions.Configuration;

namespace ComparingApp.FaceDetectionAlgorithms
{
    class UltraFaceDetection : IDetectFace
    {
        private readonly UltraFaceParameter _param;

        public UltraFaceDetection(IConfiguration configuration)
        {
            _param = new UltraFaceParameter
            {
                BinFilePath = configuration["Algos:UltraFace:BinFilePath"],
                ParamFilePath = configuration["Algos:UltraFace:ParamFilePath"],
                InputWidth = int.Parse(configuration["Algos:UltraFace:InputWidth"]),
                InputLength = int.Parse(configuration["Algos:UltraFace:InputLength"]),
                NumThread = int.Parse(configuration["Algos:UltraFace:NumThread"]),
                ScoreThreshold = float.Parse(configuration["Algos:UltraFace:ScoreThreshold"])
            };
        }
        public Mat Detect(Mat mat)
        {
            using (var ultraFace = UltraFace.Create(_param))
            {
                using var inMat = NcnnDotNet.Mat.FromPixels(mat.Data, NcnnDotNet.PixelType.Bgr2Rgb, mat.Cols, mat.Rows);
                var faceInfos = ultraFace.Detect(inMat).ToArray();
                for (var j = 0; j < faceInfos.Length; j++)
                {
                    var face = faceInfos[j];
                    mat.Rectangle(new OpenCvSharp.Rect((int)face.X1, (int)face.Y1, (int)face.X2 - (int)face.X1, (int)face.Y2 - (int)face.Y1), Scalar.Red);
                }
                return mat; 
            }
        }
    }
}
