using System;
using System.Collections.Generic;

namespace StateMachineSystem
{
    public class StateMachine
    {
        public event Action<byte> OnStateChange;

        private readonly Dictionary<byte, IState> _states = new();
        private IState _currentState;
        public byte CurrentStateId { get; private set; }

        public IState CurrentState => _currentState;

        public void AddState(byte id, IState state)
        {
            if (!_states.ContainsKey(id))
                _states.Add(id, state);
        }

        public void InitState(byte id)
        {
            if (_states.TryGetValue(id, out var state))
            {
                _currentState = state;
                CurrentStateId = id;
                _currentState.Enter();
                OnStateChange?.Invoke(id);
            }
        }

        public void SetState(byte newStateId)
        {
            if (newStateId == CurrentStateId) return;

            ChangeStateInternal(newStateId);
        }

        public void ForceState(byte newStateId)
        {
            ChangeStateInternal(newStateId);
        }

        private void ChangeStateInternal(byte newStateId)
        {
            if (_states.TryGetValue(newStateId, out var newState))
            {
                _currentState?.Exit();
                _currentState = newState;
                CurrentStateId = newStateId;
                _currentState.Enter();
                OnStateChange?.Invoke(newStateId);
            }
        }

        public void Update(float deltaTime)
        {
            _currentState?.Update(deltaTime);
        }

        public void Dispose()
        {
            _currentState?.Exit();
            _states.Clear();
        }
    }
}