using Cinemachine;
using UnityEngine;
using StateMachineSystem.ServiceLocatorSystem;
using Lesson19;

namespace Shooting
{
    public class GunAimer : MonoBehaviour
    {
        [SerializeField] private Transform _gunTransform;
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private float _rayDistance;
        [SerializeField] private LayerMask _rayMask;
        [SerializeField] private CinemachineMixingCamera _mixingCamera;
        [SerializeField] private float _aimSpeed;

        private Vector3 _hitPoint;
        private float _aimValue;
        private float _targetAimValue;

        private InputControllerr _inputController;

        // ✅ Публічна властивість без помилки
        public Vector3 AimPoint => _hitPoint;
        public float AimValue => _aimValue;

        private void Start()
        {
            _inputController = ServiceLocator.Instance.GetService<InputControllerr>();

            if (_inputController != null)
            {
                _inputController.enabled = true;
                _inputController.OnSecondaryInput += SecondaryInputHandler;
            }
            else
            {
                Debug.LogError("GunAimer: Не знайдено InputControllerr через ServiceLocator.");
            }
        }

        private void OnDestroy()
        {
            if (_inputController != null)
            {
                _inputController.OnSecondaryInput -= SecondaryInputHandler;
            }
        }

        private void FixedUpdate()
        {
            Ray ray = new Ray(_cameraTransform.position, _cameraTransform.forward);
            _hitPoint = _cameraTransform.position + _cameraTransform.forward * _rayDistance;

            if (Physics.Raycast(ray, out RaycastHit hitInfo, _rayDistance, _rayMask))
                _hitPoint = hitInfo.point;

            _gunTransform.LookAt(_hitPoint);
        }

        private void Update()
        {
            _aimValue = Mathf.MoveTowards(_aimValue, _targetAimValue, _aimSpeed * Time.deltaTime);
            _mixingCamera.m_Weight0 = 1f - _aimValue;
            _mixingCamera.m_Weight1 = _aimValue;
        }

        private void OnDrawGizmos()
        {
            if (_gunTransform != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(_gunTransform.position, _hitPoint);
            }
        }

        private void SecondaryInputHandler(bool performed)
        {
            _targetAimValue = performed ? 1f : 0f;
        }
    }
}
