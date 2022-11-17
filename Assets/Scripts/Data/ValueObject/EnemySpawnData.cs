using System;
using Enums;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public class EnemySpawnData
    {
        public EnemyTypes EnemyTypes;
        public int EnemyCount;
        //public int CurrentEnemyCount = 0;
    }
}