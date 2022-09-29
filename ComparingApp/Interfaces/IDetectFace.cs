using ComparingApp.Model;
using OpenCvSharp;

namespace ComparingApp.Interfaces
{
    public interface IDetectFace
    {
       public Mat Detect(Mat mat); 
       public AlogrithmType AlogrithmType { get; }
    }
}
