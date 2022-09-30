using OpenCvSharp;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using ComparingApp.ViewModel;

namespace ComparingApp.Utils
{
    public class CameraCapture
    {

        private readonly VideoCapture _videoCapture;
        private readonly Mat _cameraFrame;
        private readonly int cameraId; 
        private readonly AlgorithmViewModelProvider algorithms; 
        public CameraCapture(AlgorithmViewModelProvider algorithmsViewModels, IConfiguration configuration)
        {
            int.TryParse(configuration["CameraCapture:Height"], out int height);
            int.TryParse(configuration["CameraCapture:Width"], out int width);
            algorithms = algorithmsViewModels; 
            _videoCapture = new VideoCapture()
            {
                FrameHeight = height,
                FrameWidth = width
            };
            _cameraFrame = new Mat();
            algorithms = algorithmsViewModels;
            int.TryParse(configuration["CameraCapture:CameraId"], out cameraId); 
        }
        public bool OpenCamera() 
        {
            _videoCapture.Open(cameraId);
            return _videoCapture.IsOpened();
        }

        private  Mat TakePhoto()
        {
            if (!_videoCapture.IsOpened())
            {
                return new Mat("cameraProblem.jpg");
            }
            _videoCapture.Read(_cameraFrame);
            var frame = _cameraFrame.Clone();
            return frame;
        }
        public void DetectPresence()
        {
           while (true)
            {
                using (var matOne = TakePhoto())
                {
                    var listOfDetctionTask = new List<Task>(); 
                    foreach (var algo in algorithms.Get())
                    {
                        Mat matToCheck = new Mat(); 
                        matOne.CopyTo(matToCheck);
                        listOfDetctionTask.Add(new Task(() => algo.Update(matToCheck))); 
                    }
                    listOfDetctionTask.ForEach(w => w.Start());
                    listOfDetctionTask.ForEach(w => w.Wait()); 
                }
             }
        }
    }
}
