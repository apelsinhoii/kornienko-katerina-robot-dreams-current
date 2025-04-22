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
            PhysX.InputController.OnEscape += EscapeHandler;
        }

        private void EscapeHandler()
        {
            Enabled = !Enabled;
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