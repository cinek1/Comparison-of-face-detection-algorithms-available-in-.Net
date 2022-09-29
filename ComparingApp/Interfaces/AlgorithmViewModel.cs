using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows.Media.Imaging;

namespace ComparingApp.Interfaces
{
    public class AlgorithmViewModel : INotifyPropertyChanged
    {
        private BitmapImage _imageViewer;

        private readonly IDetectFace _detectFace;
        public AlgorithmViewModel(IDetectFace detect)
        {
            _detectFace = detect;
        }
        public void Update(Mat mat)
        {
            _detectFace.Detect(mat);
            Bitmap bitmapImage = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(mat);
            ImageViewer = ConvertToBitmatImage(bitmapImage);
            mat.Dispose();
        }

        public BitmapImage ImageViewer
        {
            get
            {
                return _imageViewer;
            }
            set
            {
                _imageViewer = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ImageViewer"));
            }
        }
        protected BitmapImage ConvertToBitmatImage(Bitmap bitmap)
        {
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;
                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
                return bitmapImage;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
    }
}
