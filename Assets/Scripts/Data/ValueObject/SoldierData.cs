using System;
using Enums;
using UnityEngine.Rendering;

namespace Data.ValueObject
{
    [Serializable]
    public class SoldierData
    {
        public SerializedDictionary<SoldierType, SoldierTypesData> SoldierTypeDatas;
    }
}