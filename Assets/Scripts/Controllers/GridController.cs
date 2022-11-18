﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.UnityObjects;
using Data.ValueObject;
using Enums;
using Managers;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Controllers
{
    public class GridController : MonoBehaviour
    {
        public GridStateTypes gridStateTypes;

        private SoldierType _soldierType = SoldierType.PistolSoldier;
        [SerializeField] private GameObject _nukeParticle;

        private GameObject _soldier;
        [SerializeField] private GameObject snapPoint;

        public void GridControll()
        {
            if (gridStateTypes != GridStateTypes.Placeable) return;
            _soldierType = UISignals.Instance.onGetSoldierType();
            gridStateTypes = GridStateTypes.Placed;
            _soldier = PoolSignals.Instance.onGetPoolObject?.Invoke(_soldierType.ToString(),
                snapPoint.transform);
            _soldier.transform.SetParent(snapPoint.transform);
            _soldier.transform.localPosition = new Vector3(0, 0, 0);
            SoldierSignals.Instance.onAddCurrentSoldierCount?.Invoke();
        }

        private async void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Nuke"))
            {
                _nukeParticle.SetActive(true);
                await Task.Delay(1200);
                _nukeParticle.SetActive(false);
            }
        }
    }
}