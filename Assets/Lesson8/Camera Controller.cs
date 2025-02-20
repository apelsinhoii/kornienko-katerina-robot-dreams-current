using UnityEngine;

namespace Lesson7
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform _pitchAnchor;
        [SerializeField] private Transform _yawAnchor;
        [SerializeField] private float _sensitivity;

        private float _pitch = 20f;
        private float _yaw = 0f;

        private Vector2 _lookInput;

        private void Start()
        {
            InputController.OnLookInput += LookHandler;
        }

        private void LateUpdate()
        { 
            _pitchAnchor.localRotation = Quaternion.Euler(_pitch, 0f, 0f);
            _yawAnchor.rotation = Quaternion.Euler(0f, _yaw, 0f);
        }

        private void LookHandler(Vector2 lookInput)
        {
            lookInput *= _sensitivity;
            _pitch -= lookInput.y;
            _yaw += lookInput.x;
        }

        public void SetYawAnchor(Transform yawAnchor)
        {
            _yawAnchor.rotation = yawAnchor.rotation = Quaternion.Euler(0f, _yaw, 0f);
            _yawAnchor = yawAnchor;
        }
    }
}
