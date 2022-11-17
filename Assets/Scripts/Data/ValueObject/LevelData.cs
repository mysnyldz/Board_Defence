using System;

namespace Data.ValueObject
{
    [Serializable]
    public class LevelData
    {
        public LevelIdData LevelIdData;
        public EnemySpawnListData enemySpawnListData;
        public SoldierSpawnListData soldierSpawnListData;
        //public SoldierListData SoldierListData;
    }
}