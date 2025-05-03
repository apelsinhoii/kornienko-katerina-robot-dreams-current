using System;
using System.Collections;
using BehaviourTreeSystem;
using DefendFlag;
using StateMachineSystem.Locomotion;
using StateMachineSystem.ServiceLocatorSystem;
using UnityEngine;

namespace Animation
{
    public class InteractableAnimation : MonoBehaviour
    {
        [SerializeField] private LocomotionController _locomotionController;
        [SerializeField] private Interactor _interactor;
        [SerializeField] private Animator _animator;
        [SerializeField] private string _pickupName;
        [SerializeField] private string _activateName;
        [SerializeField] private string _idleName;
        [SerializeField] private float _crossFadeTime;
        [SerializeField] private float _lockDuration;

        private int _pickupId;
        private int _activateId;
        private int _idleId;

        private YieldInstruction _lockDelay;
        private IInteractable _interactable;

        private Lesson19.InputControllerr _inputController;

        private void Start()
        {
            _inputController = ServiceLocator.Instance.GetService<Lesson19.InputControllerr>();

            _lockDelay = new WaitForSeconds(_lockDuration);

            _pickupId = Animator.StringToHash(_pickupName);
            _activateId = Animator.StringToHash(_activateName);
            _idleId = Animator.StringToHash(_idleName);

            _interactor.OnInteract += InteractHandler;
        }

        private void InteractHandler(IInteractable interactable)
        {
            _interactable = interactable;
            StartCoroutine(LockRoutine());
        }

        private IEnumerator LockRoutine()
        {
            _inputController.Lock();

            if (_locomotionController.LocomotionState != LocomotionState.Idle)
            {
                _locomotionController.OnStateChanged += LocomotionStateHandler;
            }
            else
            {
                PlayInteract();
            }

            yield return _lockDelay;

            _inputController.Unlock();
            _animator.CrossFadeInFixedTime(_idleId, _crossFadeTime);
        }

        private void LocomotionStateHandler(LocomotionState state)
        {
            _locomotionController.OnStateChanged -= LocomotionStateHandler;
            PlayInteract();
        }

        private void PlayInteract()
        {
            int animationId = _interactable.Type switch
            {
                InteractableType.PickUp => _pickupId,
                InteractableType.Activate => _activateId,
                _ => _activateId
            };

            _animator.CrossFadeInFixedTime(animationId, _crossFadeTime);
            _interactable = null;
        }
    }
}
