using System.Collections.Generic;
using System.Linq;
using Data.UnityObjects;
using Data.ValueObject;
using Enums;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Assertions.Must;

namespace Managers
{
    public class SoldierSpawnManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serializefield Variables

        #endregion

        #region Private Variables

        [ShowInInspector] private Dictionary<SoldierType,int> _currentSoldierCount;

        [ShowInInspector] private SoldierSpawnListData _spawnDatas;
        private int _currentlevel;
        private SoldierType _selectedSoldier;

        #endregion

        #endregion

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
            _currentlevel = LevelSignals.Instance.onGetLevelCount();
            _spawnDatas = Resources.Load<CD_Level>("Data/CD_Level").LevelData[_currentlevel].soldierSpawnListData;
            InitDictionary();
        }

        private void SubscribeEvents()
        {
            SoldierSignals.Instance.onGetSoldierCount += OnGetSoldierCount;
            SoldierSignals.Instance.onAddCurrentSoldierCount += OnAddCurrentSoldierCount;
        }
        
        private void UnsubscribeEvents()
        {
            SoldierSignals.Instance.onGetSoldierCount -= OnGetSoldierCount;
            SoldierSignals.Instance.onAddCurrentSoldierCount -= OnAddCurrentSoldierCount;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private bool OnGetSoldierCount()
        { 
            _selectedSoldier = UISignals.Instance.onGetSoldierType.Invoke();
            if (_spawnDatas.SpawnDatas[_selectedSoldier] > _currentSoldierCount[_selectedSoldier])
            {
                return true;
            }
            return false;
        }

        private void InitDictionary()
        {
            _currentSoldierCount = new Dictionary<SoldierType, int>();
            foreach (var VARIABLE in _spawnDatas.SpawnDatas)
            {
                _currentSoldierCount.Add(VARIABLE.Key,0);
            }
        }

        private void OnAddCurrentSoldierCount()
        {
            _currentSoldierCount[_selectedSoldier]++;
        }
        

    }
}
