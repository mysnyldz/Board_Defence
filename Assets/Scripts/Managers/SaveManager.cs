using Command.SaveLoadCommands;
using Data.ValueObject;
using Signals;
using UnityEngine;

namespace Managers
{
    public class SaveManager : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

        private LoadCommand _loadCommand;
        private SaveCommand _saveCommand;

        #endregion

        #endregion

        #region Event Subscription

        private void Awake()
        {
            Initialization();
        }

        private void Initialization()
        {
            _loadCommand = new LoadCommand();
            _saveCommand = new SaveCommand();
        }

        private void OnEnable()
        {
            Subscribe();
        }

        private void Subscribe()
        {
            SaveLoadSignals.Instance.onSaveLevelData += _saveCommand.Execute;
            SaveLoadSignals.Instance.onLoadLevelData += _loadCommand.Execute<LevelIdData>;
        }


        private void Unsubscribe()
        {
            SaveLoadSignals.Instance.onSaveLevelData -= _saveCommand.Execute;
            SaveLoadSignals.Instance.onLoadLevelData -= _loadCommand.Execute<LevelIdData>;
        }

        private void OnDisable()
        {
            Unsubscribe();
        }

        #endregion
    }
}