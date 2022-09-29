using ComparingApp.Interfaces;
using ComparingApp.Model;
using Emgu.CV;
using Emgu.CV.Dnn;
using Emgu.CV.Structure;
using Microsoft.Extensions.Configuration;
using OpenCvSharp;
using System;
using System.Runtime.InteropServices;
using Mat = OpenCvSharp.Mat;

namespace ComparingApp.FaceDetectionAlgorithms
{
    public class DnnFaceDetector : IDetectFace
    {
        private Net _net;
        private readonly int _height;
        private readonly int _width;
        private readonly double _probability;

        public AlogrithmType AlogrithmType => AlogrithmType.Dnn;

        public DnnFaceDetector(IConfiguration configuration)
        {
            int.TryParse(configuration["Algos:Dnn:Height"], out _height);
            int.TryParse(configuration["Algos:Dnn:Width"], out _width);
            double.TryParse(configuration["Algos:Dnn:Probability"], out _probability);
            _net = DnnInvoke.ReadNetFromTensorflow(configuration["Algos:Dnn:BinFilePath"], configuration["Algos:Dnn:ParamFilePath"]);
        }
        public Mat Detect(Mat mat)
        {
            byte[] array = new byte[_width * _height * mat.ElemSize()];
            Marshal.Copy(mat.DataStart, array, 0, array.Length);
            using (Image<Bgr, byte> image1 = new Image<Bgr, byte>(_width, _height))
            {
                image1.Bytes = array;
                var frame = image1.Mat;
                int cols = 640;
                int rows = 480;
                _net.SetInput(DnnInvoke.BlobFromImage(frame, 1, new System.Drawing.Size(300, 300), default(MCvScalar), false, false));
                using (Emgu.CV.Mat matt = _net.Forward())
                {
                    float[,,,] flt = (float[,,,])matt.GetData();
                    for (int x = 0; x < flt.GetLength(2); x++)
                    {
                        if (flt[0, 0, x, 2] > _probability)
                        {
                            int X1 = Convert.ToInt32(flt[0, 0, x, 3] * cols);
                            int Y1 = Convert.ToInt32(flt[0, 0, x, 4] * rows);
                            int X2 = Convert.ToInt32(flt[0, 0, x, 5] * cols);
                            int Y2 = Convert.ToInt32(flt[0, 0, x, 6] * rows);
                            mat.Rectangle(new OpenCvSharp.Rect((int)X1, (int)Y1, (int)X2 - (int)X1, (int)Y2 - (int)Y1), Scalar.Red);

                        }
                    }
                }
            }
            return mat;
        }

    }
}
