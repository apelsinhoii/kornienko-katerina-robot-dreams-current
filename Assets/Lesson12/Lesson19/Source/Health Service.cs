using BehaviourTreeSystem;
using StateMachineSystem.ServiceLocatorSystem;
using System;
using UnityEngine;
using MainMenu; // важливо!

namespace Dummies
{
    public class HealthService : MonoServiceBase, IHealthService
    {
        [SerializeField] private HealthSystem _healthSystem;

        public override Type Type => typeof(IHealthService);

        private void Awake()
        {
            base.Awake();
            if (_healthSystem == null)
            {
                _healthSystem = GetComponent<HealthSystem>();
                if (_healthSystem == null)
                {
                    Debug.LogError("HealthSystem не знайдено поруч із HealthService!");
                }
            }
        }

        public void AddCharacter(IHealth health)
        {
            if (health is Health concreteHealth)
            {
                _healthSystem.AddCharacter(concreteHealth);
            }
            else
            {
                Debug.LogError("Переданий IHealth не є Health!");
            }
        }

        public void RemoveCharacter(IHealth health)
        {
            if (health is Health concreteHealth)
            {
                _healthSystem.RemoveCharacter(concreteHealth);
            }
            else
            {
                Debug.LogError("Переданий IHealth не є Health!");
            }
        }

        public bool GetHealth(Collider collider, out Health health)
        {
            return _healthSystem.GetHealth(collider, out health);
        }

        public IHealth this[Collider collider]
        {
            get
            {
                _healthSystem.GetHealth(collider, out Health concreteHealth);
                return concreteHealth;
            }
        }
    }
}