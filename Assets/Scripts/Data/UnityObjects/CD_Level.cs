using System.Collections.Generic;
using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObjects
{
    [CreateAssetMenu(fileName = "CD_Level ", menuName = "CD_Objects/CD_Level", order = 0)]
    public class CD_Level : ScriptableObject
    {
        public List<LevelData> LevelData;
    }
}