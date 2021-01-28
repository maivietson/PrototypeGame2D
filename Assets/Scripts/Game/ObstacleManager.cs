using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrototypeGame2D.Game
{
    public class ObstacleManager : MonoBehaviour
    {
        [SerializeField] GameObject _obstaclePrefab;
        [SerializeField] float xLimit = 3.0f;

        [SerializeField] private float _timeSpawn;
        [SerializeField] private float _spawRate;

        private SceneManager _sceneManager;

        public float timeSpawn
        {
            get
            {
                return   _timeSpawn;
            }
            set
            {
                _timeSpawn = value;
            }
        }

        private void Start()
        {
            _sceneManager = FindObjectOfType<SceneManager>() as SceneManager;
            if (!_sceneManager.isGameOver)
            {
                StartSpawnObstacle();
            }
        }

        void Update()
        {
            if(_sceneManager.isGameOver)
            {
                CancelInvoke("SpawObstacle");
            }
        }

        private void StartSpawnObstacle()
        {
            InvokeRepeating("SpawObstacle", _timeSpawn, _spawRate);
        }

        private void SpawObstacle()
        {
            float randomX = Random.Range(-xLimit, xLimit);

            Vector2 spawPosition = new Vector2(randomX, transform.position.y);

            Instantiate(_obstaclePrefab, spawPosition, Quaternion.identity);
        }
    }
}
