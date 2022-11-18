using System;
using Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class EnemySignals : MonoSingleton <EnemySignals>
    {
        public Func<GameObject> onGetBasePoints = delegate { return default;};
        public Func<GameObject> onGetSpawnPoints = delegate { return default;};
        public UnityAction<int,GameObject> onTakeDamage = delegate {  };
        public Func<int> onGetTotalEnemyCount = delegate { return default;};
        public UnityAction<int> onDecreaseTotalEnemyCount = delegate {  };
    }
}