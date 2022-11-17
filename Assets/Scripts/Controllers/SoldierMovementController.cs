using Enums;
using Managers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Controllers
{
    public class SoldierMovementController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serializefield Variables

        [SerializeField] private SoldierManager manager;
        [SerializeField] private SoldierShootController soldierShootController;

        #endregion

        #region Private Variables

        private float _turretRotX;
        [ShowInInspector] private GameObject _target;
        private float _timer;

        #endregion

        #endregion

        public void SoldierRotation()
        {
            if (soldierShootController.Targets.Count >= 1)
            {
                _target = soldierShootController.Targets[0];
                if (_target != null)
                {
                    manager.transform.rotation = Quaternion.Slerp(manager.transform.rotation,
                        Quaternion.LookRotation(_target.transform.position - manager.transform.position), 0.1f);
                }
            }
        }
    }
}