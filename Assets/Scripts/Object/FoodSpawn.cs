using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PrototypeGame2D.Game;

namespace PrototypeGame2D.Object
{
    public class FoodSpawn : MonoBehaviour
    {
        #region Singleton class: FoodSpawn
        public static FoodSpawn Instance;

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
        }
        #endregion
        [SerializeField] private GameObject _foodPrefab;

        [SerializeField] private float _timeSpawn;
        [SerializeField] private float _spawnRate;

        private int _indexFoodSpawn = 0;

        private List<FoodInfo> _foodForSpawn;

        public float timeSpawn
        {
            get { return _timeSpawn; }
            set { _timeSpawn = value; }
        }

        public float spawnRate
        {
            get { return _spawnRate; }
            set { _spawnRate = value; }
        }

        public int indexFoodSpawn
        {
            get { return _indexFoodSpawn; }
            set { _indexFoodSpawn = value; }
        }

        // Start is called before the first frame update
        void Start()
        {
            //if (!GameManager.Instance.isGameOver && FoodManager.Instance.haveFoodOrder)
            //{
            //    StartSpawnFood();
            //}
            _foodForSpawn = new List<FoodInfo>();
        }

        // Update is called once per frame
        void Update()
        {
            if(GameManager.Instance.isGameOver || !FoodManager.Instance.haveFoodOrder)
            {
                CancelInvoke("SpawnFood");
            }
        }

        public void StartSpawnFood()
        {
            if (!GameManager.Instance.isGameOver && FoodManager.Instance.haveFoodOrder)
            {
                _foodForSpawn = FoodManager.Instance.AllFoodResource;
                _foodForSpawn = RandomFoodResource(_foodForSpawn);
                InvokeRepeating("SpawnFood", _timeSpawn, _spawnRate);
            }
        }

        private void SpawnFood()
        {
            if (_indexFoodSpawn < _foodForSpawn.Count)
            {
                Vector3 pos = new Vector3(transform.position.x, transform.position.y, -0.1f);
                FoodInfo food = _foodForSpawn[_indexFoodSpawn];
                GameObject foodResource = Instantiate(_foodPrefab, pos, Quaternion.identity) as GameObject;
                foodResource.GetComponent<FoodInfo>().SetFoodInfo(food.id, food.idFoodOrder, food.image);
                foodResource.GetComponent<FoodInfo>().InitFood();

                ++_indexFoodSpawn;
            }
            else
            {
                _indexFoodSpawn = 0;
            }
        } 

        public List<FoodInfo> foodForSpawn
        {
            get { return _foodForSpawn; }
            set { _foodForSpawn = value; }
        }

        private List<FoodInfo> RandomFoodResource(List<FoodInfo> foods)
        {
            List<FoodInfo> foodsRandom = new List<FoodInfo>();
            foodsRandom = foods;
            for(int i = 0; i < foodsRandom.Count; i++)
            {
                FoodInfo tmp = foodsRandom[i];
                int r = Random.Range(i, foodsRandom.Count);
                foodsRandom[i] = foodsRandom[r];
                foodsRandom[r] = tmp;
            }

            return foodsRandom;
        }
    }
}

