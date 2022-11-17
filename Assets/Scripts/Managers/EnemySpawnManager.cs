using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Data.UnityObject;
using Data.UnityObjects;
using Data.ValueObject;
using Enums;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class EnemySpawnManager : MonoBehaviour
    {
        #region Self Variables

        #region Serializefield Variables

        [SerializeField] private List<GameObject> spawnPoints;
        [SerializeField] private List<GameObject> basePoints;
        [SerializeField] private int timer;

        #endregion

        #endregion

        #region Private Variables

        private List<int> _currentEnemyCount;

        [ShowInInspector] private EnemySpawnListData _spawnDatas;
        private int _currentlevel;
        private float _enemyTimer = 0;

        private int _spawnPointId;
        private int _basePointId;

        private int _randomSpawnDatas;

        private bool isPlayed = false;

        #endregion

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += OnPlay;
            EnemySignals.Instance.onGetBasePoints += OnGetBasePoints;
            EnemySignals.Instance.onGetSpawnPoints += OnGetSpawnPoints;
        }


        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
            EnemySignals.Instance.onGetBasePoints -= OnGetBasePoints;
            EnemySignals.Instance.onGetSpawnPoints -= OnGetSpawnPoints;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void Update()
        {
            _enemyTimer += Time.deltaTime;
            if (_enemyTimer >= timer && isPlayed)
            {
                var i = CurrentEnemyCheck();
                if (i)
                {
                    EnemySpawn();
                }

                _enemyTimer = 0;
            }
        }

        private bool CurrentEnemyCheck()
        {
            var maxEnemy = _spawnDatas.SpawnDatas;
            for (int i = 0; i < _currentEnemyCount.Count; i++)
            {
                if (_currentEnemyCount[i] < maxEnemy[i].EnemyCount)
                {
                    return true;
                }
            }

            return false;
        }

        private void EnemySpawn()
        {
            RandomEnemy();
            PoolSignals.Instance.onGetPoolObject(_spawnDatas.SpawnDatas[_randomSpawnDatas].EnemyTypes.ToString(),
                spawnPoints[_spawnPointId].transform);
        }

        private void RandomEnemy()
        {
            _spawnPointId = Random.Range(0, spawnPoints.Count);
            _basePointId = _spawnPointId;
            var spawnDatasCache = _spawnDatas.SpawnDatas;
            while (true)
            {
                _randomSpawnDatas = Random.Range(0, spawnDatasCache.Count);
                if (spawnDatasCache[_randomSpawnDatas].EnemyCount <= _currentEnemyCount[_randomSpawnDatas]) continue;
                _currentEnemyCount[_randomSpawnDatas]++;
                break;
            }
        }

        private GameObject OnGetBasePoints() => basePoints[_basePointId];
        private GameObject OnGetSpawnPoints() => spawnPoints[_spawnPointId];

        private void OnPlay()
        {
            isPlayed = true;
            _currentlevel = LevelSignals.Instance.onGetLevelCount();
            _spawnDatas = Resources.Load<CD_Level>("Data/CD_Level").LevelData[_currentlevel].enemySpawnListData;
            _currentEnemyCount = new List<int>(new int[_spawnDatas.SpawnDatas.Count]);
        }
    }
}