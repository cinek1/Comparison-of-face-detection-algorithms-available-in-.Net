using ComparingApp.Interfaces;
using ComparingApp.Model;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq; 

namespace ComparingApp.ViewModel
{
    public class AlgorithmViewModelProvider
    {
        private readonly ConcurrentDictionary<AlogrithmType, AlgorithmViewModel> algorithms; 

        public AlgorithmViewModelProvider()
        {
            algorithms = new ConcurrentDictionary<AlogrithmType, AlgorithmViewModel>(); 
        }

        public AlgorithmViewModel Create(IDetectFace detectFace)
        {
            if (!algorithms.TryGetValue(detectFace.AlogrithmType, out AlgorithmViewModel algorithm))
            {
                algorithm = new AlgorithmViewModel(detectFace);
                algorithms.AddOrUpdate(detectFace.AlogrithmType, _ => algorithm, (_, __) => algorithm);
            }
            return algorithm; 
        }

        public IEnumerable<AlgorithmViewModel> Get()
        {
            return algorithms.Select(w => w.Value); 
        }
    }
}
