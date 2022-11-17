using System;
using Extensions;
using UnityEngine;

namespace Signals
{
    public class LevelSignals : MonoSingleton<LevelSignals>
    {
        public Func<int> onGetLevelCount = delegate { return default;};
    }
}