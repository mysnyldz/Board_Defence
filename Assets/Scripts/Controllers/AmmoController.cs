using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class AmmoController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serializefield Variables

        [SerializeField] private SoldierType soldierType;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private List<GameObject> nukeList = new List<GameObject>();

        #endregion

        #region Private Variables

        private SoldierTypesData _data;

        #endregion

        #endregion

        private void OnDisable()
        {
            rb.velocity = Vector3.zero;
        }

        private void Start()
        {
            _data = GetData();
        }

        private SoldierTypesData GetData() =>
            Resources.Load<CD_Soldier>("Data/CD_Soldier").SoldierData.SoldierTypeDatas[soldierType];


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                switch (soldierType)
                {
                    case SoldierType.PistolSoldier:
                        PoolSignals.Instance.onReleasePoolObject?.Invoke(PoolType.PistolBullet.ToString(), gameObject);
                        EnemySignals.Instance.onTakeDamage?.Invoke(_data.Damage, other.gameObject);
                        break;
                    case SoldierType.ShotgunSoldier:
                        PoolSignals.Instance.onReleasePoolObject?.Invoke(PoolType.ShotgunBullet.ToString(), gameObject);
                        EnemySignals.Instance.onTakeDamage?.Invoke(_data.Damage, other.gameObject);
                        break;
                    case SoldierType.NukeSoldier:
                        nukeList.Add(other.gameObject);
                        break;
                }
            }
            else if (other.CompareTag("PlaceableGround"))
            {
                if (soldierType == SoldierType.NukeSoldier)
                {
                    for (int i = 0; i < nukeList.Count; i++)
                    {
                        EnemySignals.Instance.onTakeDamage?.Invoke(_data.Damage, nukeList[i]);
                    }
                    nukeList.Clear();
                    PoolSignals.Instance.onReleasePoolObject?.Invoke(PoolType.NukeBomb.ToString(), gameObject);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("RangeCollider"))
            {
                PoolSignals.Instance.onReleasePoolObject?.Invoke(PoolType.PistolBullet.ToString(), gameObject);
            }
        }
    }
}