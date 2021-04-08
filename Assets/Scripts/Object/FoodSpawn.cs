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
        [SerializeField] private GameObject[] bossIngredients;

        [SerializeField] private float _timeSpawn;
        [SerializeField] private float _spawnRate;

        private int _indexFoodSpawn = 0;
        private bool _delaySpawnFood;

        private GameObject bossIngredient;

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

        void Start()
        {
            _foodForSpawn = new List<FoodInfoSpaw>();
            _symbolForPowerUp = new List<string>();
            _delaySpawnFood = false;
        }

        // Update is called once per frame
        void Update()
        {
            if(GameManager.Instance.GetCurrentState() == STATE.STATE_GAMEOVER && !FoodManager.Instance.haveFoodOrder)
            {
                StopAllCoroutines();
            }
            else
            {
                if(GameManager.Instance.GetCurrentState() != STATE.STATE_FINAL_BOSS_SPAWN)
                {
                    if (!_delaySpawnFood)
                    {
                        _delaySpawnFood = true;
                        StartSpawnFood();
                    }
                }
            }
        }

        public void StartSpawnFood()
        {
            if (GameManager.Instance.GetCurrentState() != STATE.STATE_PAUSE && FoodManager.Instance.haveFoodOrder)
            {
                _foodForSpawn = FoodManager.Instance.FoodInfoSpaws;

                StartCoroutine("SpawnFoodResource");
            }
        }

        public void PauseSpawnFood()
        {
            StopCoroutine("SpawnFoodResource");
        }

        public void ResetIngredientSpawn()
        {
            //StopCoroutine("SpawnFoodResource");
            StopAllCoroutines();
            _foodForSpawn.Clear();
            _indexFoodSpawn = 0;
        }

        public void StartSpawnBoss(THEME theme)
        {
            switch(theme)
            {
                case THEME.THEME_USA:
                    bossIngredient = bossIngredients[1];
                    break;
                case THEME.THEME_ITALY:
                    bossIngredient = bossIngredients[2];
                    break;
                case THEME.THEME_JAPAN:
                default:
                    bossIngredient = bossIngredients[0];
                    break;
            }
            StartCoroutine("SpawnBossIngredients");
        }

        public void PowerupStartSpawnFoodWithOneSymbol()
        {
            _symbolForPowerUp = RandomSymbol();
            StartCoroutine("SpawnFoodWithOneSymbol");
        }

        private IEnumerator SpawnBossIngredients()
        {
            FoodInfoSpaw food = _foodForSpawn[_indexFoodSpawn];

            GameObject boss = Instantiate(bossIngredient, transform.position, Quaternion.identity) as GameObject;
            boss.transform.localScale = new Vector3(0.4f, 0.4f);
            boss.GetComponent<SpriteRenderer>().sprite = food.Image;
            boss.name = food.ID;
            ++_indexFoodSpawn;
            yield return new WaitForSeconds(10);
            FoodManager.Instance.delaySpawnIngredientsBoss = false;
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
                if (food.typeDish == TypeDish.DISH_SEMI)
                {
                    foodResource.GetComponent<FoodInfoSpaw>().PlayEffect();
                }
                else
                {
                    foodResource.GetComponent<FoodInfoSpaw>().StopEffect();
                }
                if (_symbolForPowerUp.Count > 0)
                {
                    foodResource.GetComponent<FoodInfoSpaw>().SetFoodSpawn(food.ID, food.Image, _symbolForPowerUp, food.typeDish);
                }
                else
                {
                    if(food.typeDish == TypeDish.DISH_SEMI)
                    {
                        foodResource.GetComponent<FoodInfoSpaw>().SetFoodSpawn(food.ID, food.Image, RandomSymbolSemi(), food.typeDish);
                    }
                    else
                    {
                        foodResource.GetComponent<FoodInfoSpaw>().SetFoodSpawn(food.ID, food.Image, RandomSymbol(), food.typeDish);
                    }
                }
                foodResource.GetComponent<SpriteRenderer>().sprite = food.Image;
                foodResource.GetComponent<FoodInfoSpaw>().InitSymbol();
                ++_indexFoodSpawn;
                yield return new WaitForSeconds(_timeSpawn);
            }
            else
            {
                _indexFoodSpawn = 0;
            }

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
                foodResource.GetComponent<FoodInfoSpaw>().SetFoodSpawn(food.ID, food.Image, RandomSymbol(), food.typeDish);
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

        private List<string> RandomSymbolSemi()
        {
            List<string> random = new List<string>();
            string symbolRan = Symbols.GetRandomSymbolSemi();
            random.Add(symbolRan);

            return random;
        }
    }
}

