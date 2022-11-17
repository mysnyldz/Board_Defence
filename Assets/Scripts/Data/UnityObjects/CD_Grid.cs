using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Grid", menuName = "CD_Objects/CD_Grid", order = 0)]
    public class CD_Grid : ScriptableObject
    {
        public GridData data;
    }
}