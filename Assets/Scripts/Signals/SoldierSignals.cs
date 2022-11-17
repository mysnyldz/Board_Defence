using System;
using Enums;
using Extensions;
using Managers;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class SoldierSignals : MonoSingleton<SoldierSignals>
    {
        public Func<SoldierType> onGetSoldierType = delegate { return default; };
        public UnityAction<GameObject> onEnemyRemoveTargetList = delegate { };
        public Func<bool> onGetSoldierCount  = delegate { return default; };
        public UnityAction onAddCurrentSoldierCount = delegate {  };
    }
}