using System;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Signals;
using Sirenix.OdinInspector;
using UnityEditor.ShortcutManagement;
using UnityEngine;
using UnityTemplateProjects.Controller;

namespace Managers
{
    public class GridManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serializefield Variables

        [SerializeField] private GridCreatorController gridCreatorController;

        #endregion

        #region Private Variables

        [ShowInInspector] private GridData _data;
        private int _width;
        private int _height;
        private int _cellSpaceSize;
        private GameObject[,] _gridArray;

        #endregion

        #endregion

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += OnPlay;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion


        private void Start()
        {
            _data = GetData();
            GetReferences();
        }

        private void OnPlay()
        {
            gridCreatorController.GridCreator(_width, _height, _cellSpaceSize);
        }

        private GridData GetData() => Resources.Load<CD_Grid>("Data/CD_Grid").data;

        private void GetReferences()
        {
            _width = _data.Width;
            _height = _data.Height;
            _cellSpaceSize = _data.CellSpaceSize;
        }
    }
}