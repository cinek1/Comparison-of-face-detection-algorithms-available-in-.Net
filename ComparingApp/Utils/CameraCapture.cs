using Autofac.Features.Indexed;
using ComparingApp.Interfaces;
using OpenCvSharp;
using System;
using System.Threading.Tasks;
using System.Collections.Generic; 
namespace ComparingApp.Utils
{
    public class CameraCapture
    {

        private readonly VideoCapture _videoCapture;
        private readonly Mat _cameraFrame;
        private IIndex<Model.AlogrithmType, AlgorithmViewModel> algorithms; 
        public CameraCapture(IIndex<Model.AlogrithmType, AlgorithmViewModel> algorithmsViewModels)
        {
            algorithms = algorithmsViewModels; 
            _videoCapture = new VideoCapture();
            _cameraFrame = new Mat();
            algorithms = algorithmsViewModels; 
        }
        public bool OpenCamera() 
        {
            _videoCapture.Open(0);
            return _videoCapture.IsOpened();
        }

        private  Mat TakePhoto()
        {
            if (!_videoCapture.IsOpened())
            {
                throw new Exception("DetectPresence: videoCapture is not opened");
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
                    List<Task> listOfDetctionTask = new List<Task>(); 
                    foreach (var algo in Enum.GetValues(typeof(Model.AlogrithmType)))
                    {
                        Mat matToCheck = new Mat(); 
                        matOne.CopyTo(matToCheck);
                        listOfDetctionTask.Add(new Task(() => algorithms[(Model.AlogrithmType)algo].Update(matToCheck))); 
                    }
                    listOfDetctionTask.ForEach(w => w.Start());
                    listOfDetctionTask.ForEach(w => w.Wait()); 
                }
             }
        }
    }
}
