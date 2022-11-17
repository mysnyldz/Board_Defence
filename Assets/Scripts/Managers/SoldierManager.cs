using System;
using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Signals;
using UnityEditor.AnimatedValues;
using UnityEngine;

namespace Managers
{
    public class SoldierManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public SoldierType SoldierType;
        public SoldierAnimTypes SoldierAnim;
        public bool AnimValue;

        #endregion

        #region Serializefield Variables

        [SerializeField] private SoldierMovementController soldierMovementController;
        [SerializeField] private SoldierShootController soldierShootController;
        [SerializeField] private SoldierAnimationController soldierAnimationController;
        [SerializeField] private SoldierType _type;

        #endregion

        #region Private Variables

        private SoldierData _data;

        #endregion

        #endregion

        private void Awake()
        {
            _data = GetSoldierData();
        }


        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            SoldierSignals.Instance.onGetSoldierType += OnGetSoldierType;
            SoldierSignals.Instance.onEnemyRemoveTargetList += OnEnemyRemoveTargetList;
        }


        private void UnsubscribeEvents()
        {
            SoldierSignals.Instance.onGetSoldierType -= OnGetSoldierType;
            SoldierSignals.Instance.onEnemyRemoveTargetList -= OnEnemyRemoveTargetList;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private SoldierData GetSoldierData() => Resources.Load<CD_Soldier>("Data/CD_Soldier").SoldierData;

        public void Attack()
        {
            switch (SoldierType)
            {
                case SoldierType.Pistol:
                    soldierShootController.isAttack(PoolType.PistolBullet);
                    break;
                case SoldierType.ShotGun:
                    soldierShootController.isAttack(PoolType.ShotgunBullet);
                    break;
                case SoldierType.Knife:
                    soldierShootController.isAttack(PoolType.Nuke);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            //soldierAnimationController.SetAnim(SoldierAnim);
            soldierAnimationController.SetBoolAnim(SoldierAnim, AnimValue);
        }

        // public void CloseAttack()
        // {
        //     soldierShootController.CloseAttack();
        //     soldierAnimationController.SetAnim(SoldierAnim, AnimValue);
        // }

        private void OnEnemyRemoveTargetList(GameObject obj)
        {
            soldierShootController.OnRemoveTargetList(obj);
        }

        private SoldierType OnGetSoldierType()
        {
            return _type;
        }
    }
}