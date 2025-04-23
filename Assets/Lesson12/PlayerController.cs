using System;
using UnityEngine;
using Lesson19;
using StateMachineSystem.ServiceLocatorSystem;

namespace PhysX
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float _speed;

        private Vector3 _localDirection;
        private Transform _transform;

        private InputControllerr _inputController;

        private void Awake()
        {
            _transform = transform;
        }

        private void Start()
        {
            _inputController = ServiceLocator.Instance.GetService<InputControllerr>();

            if (_inputController != null)
            {
                _inputController.enabled = true; // ВАЖЛИВО: увімкнути, щоб події почали працювати!
                _inputController.OnMoveInput += MoveHandler;
            }
            else
            {
                Debug.LogError("PlayerController: Не вдалося знайти InputControllerr через ServiceLocator.");
            }
        }

        private void OnDestroy()
        {
            if (_inputController != null)
            {
                _inputController.OnMoveInput -= MoveHandler;
            }
        }

        private void MoveHandler(Vector2 input)
        {
            _localDirection = new Vector3(input.x, 0, input.y);
        }

        private void FixedUpdate()
        {
            Vector3 forward = _transform.forward;
            Vector3 right = _transform.right;

            Vector3 direction = forward * _localDirection.z + right * _localDirection.x;
            _characterController.SimpleMove(direction * _speed);
        }
    }
}