using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public class EnemySpawnListData
    {
        public List<EnemySpawnData> SpawnDatas;
    }
}