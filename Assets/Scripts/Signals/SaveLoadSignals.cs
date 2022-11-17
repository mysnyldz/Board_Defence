using System;
using Data.ValueObject;
using Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class SaveLoadSignals : MonoSingleton<SaveLoadSignals>
    {
        public UnityAction<LevelIdData,int> onSaveLevelData = delegate {  };

        public Func<string,int, LevelIdData> onLoadLevelData;

    }
}