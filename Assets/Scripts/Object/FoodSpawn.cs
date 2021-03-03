using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PrototypeGame2D.Game;
using System.Linq;
using PrototypeGame2D.Core;

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
        private bool _delaySpawnFood;

        private List<FoodInfoSpaw> _foodForSpawn;
        private List<string> _symbolForPowerUp;

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

        //public List<string> foodDestroied
        //{
        //    get { return _foodDestroied; }
        //    set { _foodDestroied = value; }
        //}

        // Start is called before the first frame update
        void Start()
        {
            _foodForSpawn = new List<FoodInfoSpaw>();
            _symbolForPowerUp = new List<string>();
            _delaySpawnFood = false;
        }

        // Update is called once per frame
        void Update()
        {
            if(GameManager.Instance.isGameOver && !FoodManager.Instance.haveFoodOrder)
            {
                //CancelInvoke("SpawnFood");
                StopAllCoroutines();
            }
            else
            {
                if(!_delaySpawnFood)
                {
                    _delaySpawnFood = true;
                    StartSpawnFood();
                }
            }
        }

        public void StartSpawnFood()
        {
            if (!GameManager.Instance.isGameOver && FoodManager.Instance.haveFoodOrder)
            {
                _foodForSpawn = FoodManager.Instance.FoodInfoSpaws;
                //_foodForSpawn = RandomFoodResource(_foodForSpawn);
                //InvokeRepeating("SpawnFood", _timeSpawn, _spawnRate);

                StartCoroutine("SpawnFoodResource");
            }
        }

        public void PowerupStartSpawnFoodWithOneSymbol()
        {
            _symbolForPowerUp = RandomSymbol();
            StartCoroutine("SpawnFoodWithOneSymbol");
        }

        private IEnumerator SpawnFoodWithOneSymbol()
        {
            yield return new WaitForSeconds(Defination.TIME_POWER_UP_ONLY_SYMBOL);
            _symbolForPowerUp.Clear();
        }

        private IEnumerator SpawnFoodResource()
        {
            if (FoodManager.Instance.refreshFoodResource)
            {
                //_foodForSpawn = RandomFoodResource(FoodManager.Instance.FoodInfoSpaws);
                _foodForSpawn = FoodManager.Instance.FoodInfoSpaws;
                FoodManager.Instance.refreshFoodResource = false;
            }

            if (_indexFoodSpawn < _foodForSpawn.Count)
            {
                FoodInfoSpaw food = _foodForSpawn[_indexFoodSpawn];

                GameObject foodResource = Instantiate(_foodPrefab, transform.position, Quaternion.identity) as GameObject;
                foodResource.transform.localScale = new Vector3(0.4f, 0.4f);
                foodResource.name = food.ID;
                if(_symbolForPowerUp.Count > 0)
                {
                    foodResource.GetComponent<FoodInfoSpaw>().SetFoodSpawn(food.ID, food.Image, _symbolForPowerUp);
                }
                else
                {
                    foodResource.GetComponent<FoodInfoSpaw>().SetFoodSpawn(food.ID, food.Image, RandomSymbol());
                }
                foodResource.GetComponent<SpriteRenderer>().sprite = food.Image;
                foodResource.GetComponent<FoodInfoSpaw>().InitSymbol();
                ++_indexFoodSpawn;
            }
            else
            {
                _indexFoodSpawn = 0;
            }
            yield return new WaitForSeconds(_timeSpawn);
            _delaySpawnFood = false;
        }

        private void SpawnFood()
        {
            if (FoodManager.Instance.refreshFoodResource)
            {
                //_foodForSpawn = RandomFoodResource(FoodManager.Instance.FoodInfoSpaws);
                _foodForSpawn = FoodManager.Instance.FoodInfoSpaws;
                FoodManager.Instance.refreshFoodResource = false;
            }

            if (_indexFoodSpawn < _foodForSpawn.Count)
            {
                FoodInfoSpaw food = _foodForSpawn[_indexFoodSpawn];

                GameObject foodResource = Instantiate(_foodPrefab, transform.position, Quaternion.identity) as GameObject;
                foodResource.name = food.ID;
                foodResource.GetComponent<FoodInfoSpaw>().SetFoodSpawn(food.ID, food.Image, RandomSymbol());
                foodResource.GetComponent<SpriteRenderer>().sprite = food.Image;
                foodResource.GetComponent<FoodInfoSpaw>().InitSymbol();
                ++_indexFoodSpawn;
            }
            else
            {
                _indexFoodSpawn = 0;
            }
        } 

        private List<FoodInfoSpaw> RandomFoodResource(List<FoodInfoSpaw> foods)
        {
            List<FoodInfoSpaw> foodsRandom = new List<FoodInfoSpaw>();
            foodsRandom = foods;
            for(int i = 0; i < foodsRandom.Count; i++)
            {
                FoodInfoSpaw tmp = foodsRandom[i];
                int r = Random.Range(i, foodsRandom.Count);
                foodsRandom[i] = foodsRandom[r];
                foodsRandom[r] = tmp;
            }

            return foodsRandom;
        }

        private List<string> RandomSymbol()
        {
            List<string> random = new List<string>();
            string symbolRan = Symbols.GetRandomSymbol();
            random.Add(symbolRan);

            return random;
        }
    }
}

