using System;
using System.Collections;
using Abstract;
using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Signals;
using Sirenix.OdinInspector;
using States.EnemyStates;
using UnityEngine;
using UnityEngine.AI;

namespace Managers
{
    public class EnemyManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public GameObject BasePoints;
        public GameObject SpawnPoints;

        #endregion

        #region Serializefield Variables

        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private EnemyTypes types;
        [SerializeField] private EnemyAnimationController animationController;

        #endregion

        #region Private Variables

        [ShowInInspector] private int _health;
        [ShowInInspector] private EnemyAnimTypes _enemyAnimTypes;
        private EnemyBaseState _currentEnemyBaseState;
        private EnemyMoveBaseState _enemyMoveBaseState;
        private EnemyDeathState _enemyDeathState;
        private EnemyData _data;

        #endregion

        #endregion

        private void Awake()
        {
            GetReferences();
        }

        private void OnEnable()
        {
            _health = _data.EnemyTypeDatas[types].Health;
            BasePoints = EnemySignals.Instance.onGetBasePoints?.Invoke();
            SpawnPoints = EnemySignals.Instance.onGetSpawnPoints?.Invoke();
            _currentEnemyBaseState = _enemyMoveBaseState;
            _currentEnemyBaseState.EnterState();
            SubscribeEvents();
        }

        #region Event Subscription

        private void SubscribeEvents()
        {
            EnemySignals.Instance.onTakeDamage += OnTakeDamage;
            CoreGameSignals.Instance.onReset += OnReset;
        }


        private void UnsubscribeEvents()
        {
            EnemySignals.Instance.onTakeDamage -= OnTakeDamage;
            CoreGameSignals.Instance.onReset -= OnReset;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion


        private void GetReferences()
        {
            var manager = this;
            _data = Resources.Load<CD_Enemy>("Data/CD_Enemy").EnemyData;
            _enemyMoveBaseState = new EnemyMoveBaseState(ref manager, ref agent, ref _data, ref types);
            _enemyDeathState = new EnemyDeathState(ref manager, ref agent, ref _data, ref types);
        }

        private void Update()
        {
            _currentEnemyBaseState.UpdateState();
        }


        private void OnTriggerEnter(Collider other)
        {
            _currentEnemyBaseState.OnTriggerEnter(other);
        }

        private void OnTriggerExit(Collider other)
        {
            _currentEnemyBaseState.OnTriggerExit(other);
        }

        public void SetTriggerAnim(EnemyAnimTypes animTypestypes)
        {
            _enemyAnimTypes = animTypestypes;
            animationController.SetAnim(animTypestypes);
        }

        public bool Health()
        {
            return _health <= 0;
        }

        public void OnTakeDamage(int damage, GameObject obj)
        {
            if (gameObject == obj)
            {
                _health -= damage;
            }
        }


        public void SwitchState(EnemyStatesTypes state)
        {
            switch (state)
            {
                case EnemyStatesTypes.MoveBase:
                    _currentEnemyBaseState = _enemyMoveBaseState;
                    break;
                case EnemyStatesTypes.Death:
                    _currentEnemyBaseState = _enemyDeathState;
                    break;
                
            }

            _currentEnemyBaseState.EnterState();
        }
        private void OnReset()
        {
            PoolSignals.Instance.onReleasePoolObject.Invoke(types.ToString(),gameObject);
        }
    }
}
