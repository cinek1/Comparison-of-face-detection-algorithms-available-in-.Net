using ComparingApp.Interfaces;

namespace ComparingApp.ViewModel
{
    public class AlgorithmViewModelFactory
    {
        public AlgorithmViewModel Create(IDetectFace detectFace)
        {
            return new AlgorithmViewModel(detectFace); 
        }
    }
}
