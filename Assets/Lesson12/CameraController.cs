using UnityEngine;

namespace PhysX
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform _pitchAnchor;
        [SerializeField] private Transform _jawAnchor;
        [SerializeField] private float _sensitivity;
    
        private float _pitch = 0f;
        private float _jaw = 20f;
    
        private Vector2 _lookInput;

        private void Start()
        {
            InputController.OnLookInput += LookHandler;
        }

        private void LateUpdate()
        {
            _pitchAnchor.localRotation = Quaternion.Euler(0f, _pitch, 0f);
            _jawAnchor.localRotation = Quaternion.Euler(_jaw, 0f, 0f);
        }

        private void LookHandler(Vector2 lookInput)
        {
            lookInput *= _sensitivity;
            _pitch += lookInput.x;
            _jaw += -lookInput.y;
        }
    }
}