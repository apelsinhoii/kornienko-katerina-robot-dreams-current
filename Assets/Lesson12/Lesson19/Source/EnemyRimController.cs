using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTreeSystem
{
    public enum EnemyState : byte
    {
        Idle = 0,
        Patrol = 1,
        Attack = 2,
        Death = 3
    }

    public class EnemyRimController : MonoBehaviour
    {
        [SerializeField] private Renderer _rimRenderer;
        [SerializeField] private string _rimColorProperty = "_RimColor";

        private Dictionary<EnemyState, Color> _stateColors;

        private void Awake()
        {
            if (_rimRenderer == null)
            {
                _rimRenderer = GetComponentInChildren<Renderer>();
                if (_rimRenderer == null)
                {
                    Debug.LogError("EnemyRimController: Renderer не знайдено!");
                }
            }

            InitializeStateColors();
        }

        private void InitializeStateColors()
        {
            _stateColors = new Dictionary<EnemyState, Color>
            {
                { EnemyState.Idle, Color.blue },
                { EnemyState.Patrol, Color.green },
                { EnemyState.Attack, Color.red },
                { EnemyState.Death, Color.black }
            };
        }

        public void SetRimColor(byte stateID)
        {
            EnemyState state = (EnemyState)stateID;

            if (_stateColors.TryGetValue(state, out Color color))
            {
                ApplyColor(color);
            }
            else
            {
                Debug.LogWarning($"EnemyRimController: Колір для стану {state} не знайдено!");
            }
        }

        private void ApplyColor(Color color)
        {
            if (_rimRenderer != null && _rimRenderer.material.HasProperty(_rimColorProperty))
            {
                _rimRenderer.material.SetColor(_rimColorProperty, color);
            }
            else
            {
                Debug.LogWarning("EnemyRimController: Матеріал не має властивості для встановлення кольору!");
            }
        }
    }
}