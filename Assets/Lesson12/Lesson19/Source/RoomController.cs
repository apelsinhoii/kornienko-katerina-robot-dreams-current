using System;
using System.Collections.Generic;
using Dummies;
using BehaviourTreeSystem;
using MainMenu;
using StateMachineSystem.ServiceLocatorSystem;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace BehaviourTreeSystem
{
    public class RoomController : MonoBehaviour, INavPointProvider
    {
        [Header("Room Settings")]
        [SerializeField] private Vector3 _roomExtends;
        [SerializeField] private Vector3 _roomOffset;

        [Header("Enemy Settings")]
        [SerializeField] private EnemyController _enemyPrefab; 
        [SerializeField] private int _maxEnemies = 5;
        [SerializeField] private float _spawnDelay = 3f;

        private List<EnemyController> _enemies;
        private readonly Vector3[] _gizmosPoints = new Vector3[4];

        private Vector3 _point;
        private NavMeshHit _hit;

        private IHealthService _healthService;
        private ICameraService _cameraService;

        private float _time;

        private void Start()
        {
            _healthService = ServiceLocator.Instance.GetService<IHealthService>();
            _cameraService = ServiceLocator.Instance.GetService<ICameraService>();
            
            if (_healthService == null)
                Debug.LogError("HealthService НЕ знайдено через ServiceLocator!");

            if (_cameraService == null)
                Debug.LogError("CameraService НЕ знайдено через ServiceLocator!");

            _enemies = new List<EnemyController>(_maxEnemies);
            _time = 0f;

            SpawnEnemies(_maxEnemies);
        }

        private void Update()
        {
            if (_time < _spawnDelay)
            {
                _time += Time.deltaTime;
                return;
            }

            _time = 0f;
            int enemiesToSpawn = _maxEnemies - _enemies.Count;
            if (enemiesToSpawn > 0)
            {
                SpawnEnemies(enemiesToSpawn);
            }
        }

        private void SpawnEnemies(int count)
        {
            for (int i = 0; i < count; i++)
            {
                SpawnEnemy();
            }
        }

        [ContextMenu("Spawn Enemy")]
        private void SpawnEnemy()
        {
            if (_enemyPrefab == null)
            {
                Debug.LogError("Enemy Prefab не призначено!");
                return;
            }

            GetValidPoint();

            if (!_hit.hit)
            {
                Debug.LogWarning("Не знайшли правильну позицію на NavMesh для ворога!");
                return;
            }

            EnemyController enemy = Instantiate(_enemyPrefab, _hit.position, Quaternion.identity);
            enemy.Initialize(this, _cameraService.Camera);

            _healthService?.AddCharacter(enemy.Health);
            enemy.Health.OnDeath += () => EnemyDeathHandler(enemy);

            _enemies.Add(enemy);
        }

        private void GetValidPoint()
        {
            int attempts = 10; 
            bool found = false;

            for (int i = 0; i < attempts; i++)
            {
                Vector3 center = transform.position + _roomOffset;
                Vector3 min = center - _roomExtends;
                Vector3 max = center + _roomExtends;

                _point.x = Random.Range(min.x, max.x);
                _point.y = Random.Range(min.y, max.y);
                _point.z = Random.Range(min.z, max.z);

                if (NavMesh.SamplePosition(_point, out _hit, 2.0f, NavMesh.AllAreas))
                {
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                _hit.hit = false;
            }
        }

        private void EnemyDeathHandler(EnemyController enemy)
        {
            if (_enemies.Contains(enemy))
            {
                _enemies.Remove(enemy);
            }

            if (enemy != null)
            {
                Destroy(enemy.gameObject);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Vector3 center = transform.position + _roomOffset;

            Vector3 position = center;
            position.x += _roomExtends.x;
            position.z += _roomExtends.z;
            _gizmosPoints[0] = position;

            position = center;
            position.x += _roomExtends.x;
            position.z -= _roomExtends.z;
            _gizmosPoints[1] = position;

            position = center;
            position.x -= _roomExtends.x;
            position.z -= _roomExtends.z;
            _gizmosPoints[2] = position;

            position = center;
            position.x -= _roomExtends.x;
            position.z += _roomExtends.z;
            _gizmosPoints[3] = position;

            Gizmos.DrawLineStrip(_gizmosPoints, true);

            if (_hit.hit)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(_hit.position, 0.3f);
            }
            else
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(_point, 0.3f);
            }
        }

        public Vector3 GetPoint()
        {
            GetValidPoint();
            return _hit.position;
        }
    }
}