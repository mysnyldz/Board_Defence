using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Managers;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Controllers
{
    public class SoldierShootController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public List<GameObject> Targets;

        #endregion

        #region Serializefield Variables

        [SerializeField] private SoldierManager manager;
        [SerializeField] private GameObject firePoint;
        [SerializeField] private BoxCollider collider;
        [SerializeField] private GameObject knifeCollider;

        #endregion

        #region Private Variables

        private SoldierType _soldierType;
        private Rigidbody _rb;
        [ShowInInspector] private SoldierTypesData _data = new SoldierTypesData();
        [ShowInInspector] private float _fireRate;
        private float _timer;

        #endregion

        #endregion

        private void Start()
        {
            _soldierType = SoldierSignals.Instance.onGetSoldierType.Invoke();
            _data = GetData();
            _fireRate = _data.FireRate;
            _timer = _fireRate;
            ColliderSettings();
        }

        private void ColliderSettings()
        {
            if (_data.Forward == true)
            {
                collider.center = new Vector3(0, 0.75f, _data.Range);
                collider.size = new Vector3(1.8f, 2, (_data.Range * 2) + 2);
            }
            else if (_data.All == true)
            {
                var rangeSize = _data.Range * 6;
                collider.center = new Vector3(0, 0.75f, 0);
                collider.size = new Vector3(rangeSize, _data.Range, rangeSize);
            }
        }

        private SoldierTypesData GetData() =>
            Resources.Load<CD_Soldier>("Data/CD_Soldier").SoldierData.SoldierTypeDatas[_soldierType];

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                manager.AnimValue = true;
                TargetAddList(other.gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                OnRemoveTargetList(other.gameObject);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                if (other.GetComponent<EnemyManager>().Health())
                {
                    OnRemoveTargetList(other.gameObject);
                    return;
                }

                manager.Attack();
            }
        }

        private void TargetAddList(GameObject obj)
        {
            Targets.Add(obj.gameObject);
        }

        private void TargetRemoveList(GameObject obj)
        {
            Targets.Remove(obj.gameObject);
        }


        public void isAttack(PoolType type)
        {
            _timer += Time.deltaTime;
            if (_timer >= _fireRate)
            {
                _timer = 0;
                var bullet = PoolSignals.Instance.onGetPoolObject(type.ToString(), transform);
                BulletPosition(bullet);
                _rb = bullet.GetComponent<Rigidbody>();
                _rb.AddForce(firePoint.transform.forward * 8, ForceMode.VelocityChange);
                manager.AnimValue = false;
            }
        }
        

        public void OnRemoveTargetList(GameObject obj)
        {
            if (Targets.Count > 0)
            {
                Targets.Remove(obj);
                Targets.TrimExcess();
            }
        }
        

        private void BulletPosition(GameObject bullet)
        {
            bullet.transform.position = firePoint.transform.position;
            bullet.transform.rotation = firePoint.transform.rotation;
        }
    }
}