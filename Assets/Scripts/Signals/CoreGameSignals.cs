using Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class CoreGameSignals : MonoSingleton<CoreGameSignals>
    {
        public UnityAction onLevelInitialize = delegate { };

        public UnityAction onClearActiveLevel = delegate { };
        
        public UnityAction onFailed = delegate { };
        
        public UnityAction onTryAgain = delegate {  };

        public UnityAction onNextLevel = delegate { };
        
        public UnityAction onPlay = delegate { };
        
        public UnityAction onReset = delegate { };

        public UnityAction onApplicationPause = delegate { };
        
        public UnityAction onApplicationQuit = delegate { };
        
    }
}