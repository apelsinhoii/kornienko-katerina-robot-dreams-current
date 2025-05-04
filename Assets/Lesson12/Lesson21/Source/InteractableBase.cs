using System;
using BehaviourTreeSystem;
using Dummies;
using StateMachineSystem.ServiceLocatorSystem;
using UnityEngine;

namespace DefendFlag
{
    public class InteractableBase : MonoBehaviour, IInteractable
    {
        public event Action<IInteractable> onDestroy;
        
        [SerializeField] protected GameObject rootObject;
        [SerializeField] protected Collider collider;
        [SerializeField] protected BillboardBase tooltip;

        private Transform _transform;

        public virtual InteractableType Type => InteractableType.PickUp;
        public Vector3 Position => _transform.position;
        
       private void Awake()
{
    _transform = collider.transform;

    var interactableService = ServiceLocator.Instance?.GetService<IInteractableService>();
    if (interactableService != null)
    {
        interactableService.AddInteractable(collider, this);
    }
    else
    {
        Debug.LogError("IInteractableService not found!");
    }

    if (tooltip != null)
    {
        var cameraService = ServiceLocator.Instance?.GetService<ICameraService>();
        if (cameraService != null)
        {
            tooltip.SetCamera(cameraService.Camera);
        }
        else
        {
            Debug.LogError("ICameraService not found!");
        }
        Highlight(false);
    }
    else
    {
        Debug.LogError("Tooltip is not assigned on " + gameObject.name);
    }
}


        private void OnDestroy()
        {
            Highlight(false);
            onDestroy?.Invoke(this);
            ServiceLocator.Instance?.GetService<IInteractableService>()?.RemoveInteractable(collider, this);
        }

        public virtual void Interact()
        {
            Destroy(rootObject);
        }

        public virtual void Highlight(bool active)
        {
            tooltip.gameObject.SetActive(active);
        }
    }
}