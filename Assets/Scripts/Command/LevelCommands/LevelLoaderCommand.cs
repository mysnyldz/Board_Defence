using UnityEngine;

namespace Command.LevelCommands
{
    public class LevelLoaderCommand
    {
        public void InitializeLevel(int _levelID, Transform LevelHolder)
        {
            Object.Instantiate(Resources.Load<GameObject>($"Levels/Level {_levelID}"), LevelHolder);
        }
    }
}