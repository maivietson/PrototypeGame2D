using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PrototypeGame2D.Game;
using System.Linq;

namespace PrototypeGame2D.Object
{
    public static class Extentions
    {
        public static List<T> GetClone<T>(this List<T> source)
        {
            return source.GetRange(0, source.Count);
        }
    }

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
        private List<string> _foodDestroied;

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

        public List<string> foodDestroied
        {
            get { return _foodDestroied; }
            set { _foodDestroied = value; }
        }

        // Start is called before the first frame update
        void Start()
        {
            //if (!GameManager.Instance.isGameOver && FoodManager.Instance.haveFoodOrder)
            //{
            //    StartSpawnFood();
            //}
            _foodForSpawn = new List<FoodInfo>();
            _foodDestroied = new List<string>();
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
                CloneList();
                _foodForSpawn = RandomFoodResource(_foodForSpawn);
                InvokeRepeating("SpawnFood", _timeSpawn, _spawnRate);
            }
        }

        private void SpawnFood()
        {
            if (FoodManager.Instance.refreshFoodResource)
            {
                _foodForSpawn = FoodManager.Instance.AllFoodResource;
                CloneList();
                FoodManager.Instance.refreshFoodResource = false;
            }

            if (_indexFoodSpawn < _foodForSpawn.Count)
            {
                Debug.Log(_foodDestroied.Count);
                //Vector3 pos = new Vector3(transform.position.x, transform.position.y, -0.1f);
                FoodInfo food = _foodForSpawn[_indexFoodSpawn];
                if(_foodDestroied.Count > 0)
                {
                    var result = _foodDestroied.SingleOrDefault(r => r == food.id);
                    if (result.Length > 0)
                    {
                        //Debug.Log(result.id);
                        //Debug.Log("indexFoodSpawn: " + _indexFoodSpawn);
                        _foodDestroied.Remove(result);
                        Debug.Log(_foodDestroied.Count);
                        GameObject foodResource = Instantiate(_foodPrefab, transform.position, Quaternion.identity) as GameObject;
                        foodResource.name = "FoodResource" + _indexFoodSpawn.ToString();
                        foodResource.GetComponent<FoodInfo>().SetFoodInfo(food.id, food.idFoodOrder, food.image, food.Amount, food.SymbolKey);
                        foodResource.GetComponent<FoodInfo>().InitFood();
                        ++_indexFoodSpawn;
                    }
                }
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

        private void CloneList()
        {
            foreach(FoodInfo fi in _foodForSpawn)
            {
                _foodDestroied.Add(fi.id);
            }
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

