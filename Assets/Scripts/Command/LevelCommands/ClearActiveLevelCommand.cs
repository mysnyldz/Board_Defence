using UnityEngine;

namespace Command.LevelCommands
{
    public class ClearActiveLevelCommand
    {
        public void ClearActiveBase(Transform levelHolder)
        {
            Object.Destroy(levelHolder.GetChild(0).gameObject);
        }
    }
}