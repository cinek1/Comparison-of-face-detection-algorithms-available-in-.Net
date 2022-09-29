using ComparingApp.FaceDetectionAlgorithms;
using ComparingApp.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComparingApp.Extensions
{
    public static class AlgorithmsRegistrationExtension
    {
        public static IServiceCollection AddFaceDetectionAlgorithms(this IServiceCollection services)
        {
            return services.AddSingleton<IDetectFace, HaarCascadeFaceDetector>()
                           .AddSingleton<IDetectFace, UltraFaceDetection>()
                           .AddSingleton<IDetectFace, DnnFaceDetector>()
                           .AddSingleton<IDetectFace, DlibAlgorithm>(); 
        }
    }
}
