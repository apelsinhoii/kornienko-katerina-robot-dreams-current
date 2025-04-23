using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MainMenu
{
    public class GameplayMenu : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;

        [SerializeField] private Button _confrimButton;
        [SerializeField] private Button _cancelButton;
        [SerializeField] private string _lobbySceneName;
        [SerializeField] private Lesson19.InputControllerr _inputControllerr;

        public bool Enabled
        {
            get => _canvas.enabled;
            set
            {
                if (_canvas.enabled == value)
                    return;

                _canvas.enabled = value;

                Cursor.visible = value;
                Cursor.lockState = value ? CursorLockMode.Confined : CursorLockMode.Locked;

                _inputControllerr.enabled = !value;
            }
        }

        private void Awake()
        {
            _confrimButton.onClick.AddListener(ConfirmButtonHandler);
            _cancelButton.onClick.AddListener(CancelButtonHandler);

            Enabled = false;
        }

        private void Start()
        {
            _inputControllerr.OnEscape += EscapeHandler;
            _inputControllerr.OnInventory += InventoryHandler;
        }

        private void OnDestroy()
        {
            _inputControllerr.OnEscape -= EscapeHandler;
            _inputControllerr.OnInventory -= InventoryHandler;
        }

        private void EscapeHandler()
        {
            Enabled = !Enabled;
        }

        private void InventoryHandler()
        {
            Debug.Log("Tab (Inventory) pressed");
        }

        private void ConfirmButtonHandler()
        {
            SceneManager.LoadSceneAsync(_lobbySceneName, LoadSceneMode.Single);
        }

        private void CancelButtonHandler()
        {
            Enabled = false;
        }
    }
}