using System;
using Abstract;

namespace Data.ValueObject
{
    [Serializable]
    public class LevelIdData : SaveableEntity
    {
        public static string LevelKey = "Level";

        public int LevelId;


        public LevelIdData(int _levelId)
        {
            LevelId = _levelId;
        }

        public LevelIdData()
        {
        }

        public override string GetKey()
        {
            return LevelKey;
        }
    }
}