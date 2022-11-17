using System;
using System.Collections.Generic;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Managers;
using Signals;
using Sirenix.OdinInspector.Editor.GettingStarted;
using TMPro;
using UnityEngine;

namespace Controllers
{
    public class UISoldierSelection : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serializefield Variables

        [SerializeField] private List<GameObject> SoldierList = new List<GameObject>();

        #endregion

        #region Private Variables

        private GameObject _selectionSoldier;
        private SoldierManager _soldierManager;
        private SoldierType _soldierType = SoldierType.Pistol;

        #endregion

        #endregion
        

        public void SelectionSoldier(int i)
        {
            _selectionSoldier = SoldierList[i];
            _soldierManager = _selectionSoldier.GetComponent<SoldierManager>();
            _soldierType = _soldierManager.SoldierType;
        }

        public SoldierType SelectedSoldier()
        {
            return _soldierType;
        }
    }
}