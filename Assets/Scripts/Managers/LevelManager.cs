using System;
using Command.LevelCommands;
using Data.UnityObject;
using Data.UnityObjects;
using Data.ValueObject;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Assertions.Must;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        [Header("LevelData")] public CD_Level LevelData;

        #endregion

        #region Serializefield Variables

        [SerializeField] private GameObject LevelHolder;

        #endregion

        #region Private Variables

        private LevelLoaderCommand _levelLoader;
        private ClearActiveLevelCommand _levelClearer;

        private int _levelID;
        private int _uniqueID;
        private LevelIdData levelIdData;

        #endregion

        #endregion

        private void Awake()
        {
            _levelLoader = new LevelLoaderCommand();
            _levelClearer = new ClearActiveLevelCommand();
        }

        private void Start()
        {
            GetData();
            OnInitializeLevel();
            SetLevelText();
        }


        private void GetData()
        {
            if (!ES3.FileExists($"Level{_uniqueID}.es3"))
            {
                if (!ES3.KeyExists("Level"))
                {
                    LevelData = GetLevelData();
                    Save();
                }
            }

            Load();
            // LevelData = GetBaseData();
        }


        private CD_Level GetLevelData()
        {
            return Resources.Load<CD_Level>("Data/CD_Base");
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onSuccessful += OnSuccessful;
            CoreGameSignals.Instance.onFailed += OnFailed;
            CoreGameSignals.Instance.onLevelInitialize += OnInitializeLevel;
            CoreGameSignals.Instance.onClearActiveLevel += OnClearActiveLevel;
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onApplicationQuit += OnSave;
            CoreGameSignals.Instance.onApplicationPause += OnSave;
            LevelSignals.Instance.onGetLevelCount += OnGetLevelCount;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onSuccessful -= OnSuccessful;
            CoreGameSignals.Instance.onFailed -= OnFailed;
            CoreGameSignals.Instance.onLevelInitialize -= OnInitializeLevel;
            CoreGameSignals.Instance.onClearActiveLevel -= OnClearActiveLevel;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onApplicationQuit -= OnSave;
            CoreGameSignals.Instance.onApplicationPause -= OnSave;
            LevelSignals.Instance.onGetLevelCount -= OnGetLevelCount;
        }

        private void OnDisable() 
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnSave()
        {
            Save();
        }
        
        private void SetLevelText()
        {
            
            UISignals.Instance.onSetLevelText?.Invoke(_levelID);

        }

        #region Level Management

        private void OnSuccessful()
        {
            _levelID++;
            Save();
            CoreGameSignals.Instance.onReset?.Invoke();
            SetLevelText();
        }
        private void OnReset()
        {
            CoreGameSignals.Instance.onClearActiveLevel?.Invoke();
            CoreGameSignals.Instance.onLevelInitialize?.Invoke();
            UISignals.Instance.onSetLevelText?.Invoke(_levelID);
        }

        private void OnFailed()
        {
            CoreGameSignals.Instance.onReset?.Invoke();
        }

        private void OnInitializeLevel()
        {
            _levelID = OnGetLevelCount();
            _levelLoader.InitializeLevel(_levelID, LevelHolder.transform);
        }

        private int OnGetLevelCount()
        {
            return _levelID % Resources.Load<CD_Level>("Data/CD_Level").LevelData.Count;
        }


        private void OnClearActiveLevel()
        {
            _levelClearer.ClearActiveBase(LevelHolder.transform);
        }

        #endregion

        #region Level Save and Load

        public void Save()
        {
            LevelIdData levelIdData = new LevelIdData(_levelID);
            SaveLoadSignals.Instance.onSaveLevelData.Invoke(levelIdData, _uniqueID);
        }

        public void Load()
        {
            var levelKey = LevelIdData.LevelKey;
            SaveLoadSignals.Instance.onLoadLevelData.Invoke(levelKey, _uniqueID);
        }

        #endregion
    }
}