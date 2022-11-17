using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Soldier", menuName = "CD_Objects/CD_Soldier", order = 0)]
    public class CD_Soldier : ScriptableObject
    {
        public SoldierData SoldierData;
    }
}