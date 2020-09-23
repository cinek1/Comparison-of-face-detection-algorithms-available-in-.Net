using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ComparingApp.Interfaces
{
    public interface IDetectFace
    {
       public Mat Detect(Mat mat); 
    }
}
