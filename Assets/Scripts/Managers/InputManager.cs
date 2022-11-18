using System;
using Controllers;
using Signals;
using UnityEngine;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {

        [SerializeField] private LayerMask layer;

        private RaycastHit _hit;
        private Ray _ray;
        private Vector3 _pos;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (SoldierSignals.Instance.onGetSoldierCount() != true) return;

                _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(_ray, out _hit, 1000, layer))
                {
                    if (_hit.collider.CompareTag("PlaceableGround"))
                    {
                        var _gridController = _hit.transform.GetComponent<GridController>();
                        _gridController.GridControll();
                    }
                }
            }
        }
    }
}