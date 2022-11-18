using Controllers;
using Enums;
using Signals;
using TMPro;
using UnityEngine;
using UnityTemplateProjects.Controller;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        #region Self Veriables

        #region SerializeField Variables

        [SerializeField] private UIPanelController UIPanelController;
        [SerializeField] private UISoldierSelection UISoldierSelection;

        #endregion SerializeField Variables

        #region Private Variables

        private PoolType _soldierType;

        #endregion

        #endregion Self Veriables

        #region Event Subcription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            UISignals.Instance.onOpenPanel += OnOpenPanel;
            UISignals.Instance.onClosePanel += OnClosePanel;
            UISignals.Instance.onGetSoldierType += OnGetSoldierType;
            UISignals.Instance.onQuitGame += OnQuitGame;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onNextLevel += OnNextLevel;
            CoreGameSignals.Instance.onSuccessful += OnSuccessful;
            CoreGameSignals.Instance.onTryAgain += OnTryAgain;
            CoreGameSignals.Instance.onFailed += OnFailed;
        }


        private void UnsubscribeEvents()
        {
            UISignals.Instance.onOpenPanel -= OnOpenPanel;
            UISignals.Instance.onClosePanel -= OnClosePanel;
            UISignals.Instance.onGetSoldierType -= OnGetSoldierType;
            UISignals.Instance.onQuitGame -= OnQuitGame;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onNextLevel -= OnNextLevel;
            CoreGameSignals.Instance.onSuccessful -= OnSuccessful;
            CoreGameSignals.Instance.onTryAgain -= OnTryAgain;
            CoreGameSignals.Instance.onFailed -= OnFailed;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion Event Subcription

        private void OnOpenPanel(UIPanels panels)
        {
            UIPanelController.OpenPanel(panels);
        }

        private void OnClosePanel(UIPanels panels)
        {
            UIPanelController.ClosePanel(panels);
        }

        public void OnPlay()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.MenuPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.PlayPanel);
        }

        public void OnSuccessful()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.PlayPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.CompletedPanel);
        }

        public void OnNextLevel()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.CompletedPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.PlayPanel);
        }

        public void NextLevel()
        {
            CoreGameSignals.Instance.onNextLevel?.Invoke();
        }

        public void Play()
        {
            CoreGameSignals.Instance.onPlay?.Invoke();
        }

        private SoldierType OnGetSoldierType()
        {
            return UISoldierSelection.SelectedSoldier();
        }

        public void OnTryAgain()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.TryPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.PlayPanel);
        }

        public void OnFailed()
        {
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.TryPanel);
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.PlayPanel);
        }

        public void OnQuitGame()
        {
            Application.Quit();
        }
    }
}