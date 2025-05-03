#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dummies
{
    public class HealthSystem : MonoBehaviour
    {
        public event Action<Health> OnCharacterDeath;

        [SerializeField] private Health[] _healths;

        protected Dictionary<Collider, Health> _charactersHealth = new();

        public IEnumerable<Health> CharactersHealth => _charactersHealth.Values;

        /// <summary>
        /// Editor only method
        /// </summary>
        [ContextMenu("Find Healths")]
        private void FindHealths()
        {
#if UNITY_EDITOR
            _healths = FindObjectsOfType<Health>();
            EditorUtility.SetDirty(this);
#endif
        }

        protected virtual void Awake()
        {
            for (int i = 0; i < _healths.Length; ++i)
            {
                Health health = _healths[i];
                if (health == null)
                {
                    Debug.LogWarning($"Health at index {i} is null in {gameObject.name}");
                    continue;
                }

                if (health.CharacterController == null)
                {
                    Debug.LogWarning($"Health '{health.name}' has no CharacterController in {gameObject.name}");
                    continue;
                }

                _charactersHealth.Add(health.CharacterController, health);
                health.OnDeath += () => CharacterDeathHandler(health);
            }
        }

        public virtual bool GetHealth(Collider characterController, out Health health) =>
            _charactersHealth.TryGetValue(characterController, out health);

        protected void CharacterDeathHandler(Health health)
        {
            OnCharacterDeath?.Invoke(health);
        }

        public void AddCharacter(Health health)
        {
            _charactersHealth.Add(health.CharacterController, health);
            health.OnDeath += () => CharacterDeathHandler(health);
        }

        public void RemoveCharacter(Health health)
        {
            if (_charactersHealth.ContainsKey(health.CharacterController))
            {
                _charactersHealth.Remove(health.CharacterController);
                health.OnDeath -= () => CharacterDeathHandler(health);
            }
        }
    }
}