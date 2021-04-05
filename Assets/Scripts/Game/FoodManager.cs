using PrototypeGame2D.Core;
using PrototypeGame2D.Object;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static PrototypeGame2D.Core.OrderState;

namespace PrototypeGame2D.Game
{
    public class FoodManager : MonoBehaviour
    {
        #region Singleton class: FoodManager
        public static FoodManager Instance;

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
        }

        #endregion

        [SerializeField] private Text _textSpeedSpawn;

        private List<FoodOrder> _foodOrder;
        private List<FoodInfo> _allFoodResource;

        private List<FoodInfoSpaw> _foodForSpawn;

        private bool _haveFoodOrder;
        private bool _refreshFoodResource;

        private FoodInfo _foodInfoTmp;
        private FoodOrder _foodOrderTmp;

        private bool _checking = false;

        private float _timeSacle = 0.5f;
        private int _missingOrder = 0;

        private int _numFoodComplete;
        private int _levelConveyor;

        OrderArea areaOrder;

        public List<FoodInfoSpaw> FoodInfoSpaws
        {
            get { return _foodForSpawn; }
            set { _foodForSpawn = value; }
        }

        public bool haveFoodOrder
        {
            get { return _haveFoodOrder; }
            set { _haveFoodOrder = value; }
        }

        public bool refreshFoodResource
        {
            get { return _refreshFoodResource; }
            set { _refreshFoodResource = value; }
        }

        public List<FoodInfo> AllFoodResource
        {
            get { return _allFoodResource; }
            set { _allFoodResource = value; }
        }

        public int LevelConveyor
        {
            get { return _levelConveyor; }
            set { _levelConveyor = value; }
        }

        // Start is called before the first frame update
        void Start()
        {
            _foodOrder = new List<FoodOrder>();
            _allFoodResource = new List<FoodInfo>();
            _foodForSpawn = new List<FoodInfoSpaw>();

            _refreshFoodResource = false;
            _haveFoodOrder = false;

            _missingOrder = 0;
            _numFoodComplete = 0;
            _levelConveyor = 0;

            _foodInfoTmp = new FoodInfo();
            _foodOrderTmp = new FoodOrder();

            areaOrder = FindObjectOfType<OrderArea>();
        }

        // Update is called once per frame
        void Update()
        {
            switch(GameManager.Instance.GetCurrentState())
            {
                case STATE.STATE_PLAY:
                    if (_foodOrder.Count > 0)
                    {
                        for (int i = 0; i < _foodOrder.Count; i++)
                        {
                            if (_foodOrder[i].statusOrder == STATUS.FOOD_NOT_COMPLETE)
                            {
                                if (_foodOrder[i].timeOrder > 0)
                                {
                                    _foodOrder[i].CountDownTime(Time.deltaTime * _timeSacle);
                                }
                                else
                                {
                                    _foodOrder[i].timeOrder = 0;
                                    _foodOrder[i].statusOrder = STATUS.FOOD_MISSING;
                                }
                                areaOrder.UpdateProgressBar();
                            }
                        }

                        _foodOrder = _foodOrder.OrderBy(item => item.timeOrder).ToList();
                    }
                    break;
                case STATE.STATE_PAUSE:
                    {
                        FoodSpawn.Instance.PauseSpawnFood();
                    }
                    break;
                case STATE.STATE_RESUME:
                    {
                        GameManager.Instance.SetState(STATE.STATE_PLAY);
                        FoodSpawn.Instance.StartSpawnFood();
                    }
                    break;
                case STATE.STATE_FINAL_BOSS:
                    {
                        _haveFoodOrder = false;
                        if (_foodOrder.Count == 0)
                        {
                            FoodInfoSpaw[] ingredientsSpaw = FindObjectsOfType<FoodInfoSpaw>();
                            if (ingredientsSpaw.Length == 0)
                            {
                                FoodSpawn.Instance.ResetIngredientSpawn();
                                GameManager.Instance.AppearBoss();
                                if(_haveFoodOrder)
                                {
                                    FoodSpawn.Instance.StartSpawnBoss(GameManager.Instance.GetCurrentTheme());
                                }
                            }
                        }
                    }
                    break;
                case STATE.STATE_CHANGE_THEME:
                    {
                        if(_foodOrder.Count == 0)
                        {
                            FoodInfoSpaw[] ingredientsSpaw = FindObjectsOfType<FoodInfoSpaw>();
                            if (ingredientsSpaw.Length == 0)
                            {
                                FoodSpawn.Instance.ResetIngredientSpawn();
                                GameManager.Instance.ChangeTheme();
                            }
                        }
                    }
                    break;
                default:
                case STATE.STATE_START:
                    if (_haveFoodOrder)
                    {
                        GameManager.Instance.SetState(STATE.STATE_PLAY);
                        FoodSpawn.Instance.StartSpawnFood();
                    }
                    break;
            }
        }

        public void OrderFood(FoodOrder order)
        {
            AddOrder(order);
            _haveFoodOrder = true;
        }

        public void RemoveOrder(FoodOrder foodOrder)
        {
            if(foodOrder.statusOrder == STATUS.FOOD_MISSING)
            {
                GameManager.Instance.MissingOrder();
            }
            _foodOrder.Remove(foodOrder);
        }

        public void HandleFood(string id)
        {
            _refreshFoodResource = true;

            float minTime = 1000;

            foreach (FoodOrder fo in _foodOrder)
            {
                bool existFood = fo.foodResource.Any(i => i.id == id);
                if(existFood)
                {
                    if (fo.statusOrder == STATUS.FOOD_NOT_COMPLETE)
                    {
                        //var fi = fo.foodResource.Where(i => i.id == id).FirstOrDefault();
                        var fi = FindIngredient(fo, id);
                        if (fo.timeOrder < minTime)
                        {
                            if (fi.Amount > 0)
                            {
                                minTime = fo.timeOrder;
                                _foodOrderTmp = fo;
                                _foodInfoTmp = fi;
                            }
                            else
                            {
                                minTime = 1000;
                            }
                        }
                    }
                }
            }

            HandleOrderFood(_foodOrderTmp, _foodInfoTmp);
        }

        private void HandleOrderFood(FoodOrder order, FoodInfo food)
        {
            order.haveUpdate = true;
            if (food.Amount > 0)
            {
                food.Amount -= 1;
                food.HaveUpdate = true;
                for (int i = 0; i < _foodForSpawn.Count; i++)
                {
                    if (_foodForSpawn[i].ID.Equals(food.id))
                    {
                        _foodForSpawn.RemoveAt(i);
                        break;
                    }
                }
                order.CompletePartProgressOrder();
            }

            UpdateSlotOrder(order);
        }

        public void UpdateSlotOrder(FoodOrder order)
        {
            areaOrder.UpdateProgress(order);
        }

        public void AddOrder(FoodOrder foodOrder)
        {
            _foodOrder.Add(foodOrder);

            foreach(FoodInfo fi in foodOrder.foodResource)
            {
                for(int i = 0; i < fi.Amount; i++)
                {
                    FoodInfoSpaw fis = new FoodInfoSpaw();
                    fis.SetFoodSpawn(fi.id, fi.image, fi.SymbolKey, foodOrder.typeDish);
                    _foodForSpawn.Add(fis);
                }
            }
        }

        private FoodInfo FindIngredient(FoodOrder fo, string id)
        {
            foreach(FoodInfo fi in fo.foodResource)
            {
                if(fi.id.Equals(id))
                {
                    return fi;
                }
            }
            return null;
        }

        public void CompleteOrder(FoodOrder order)
        {
            for(int i = 0; i < _foodOrder.Count; i++)
            {
                if(_foodOrder[i].Name.Equals(order.Name))
                {
                    _foodOrder.RemoveAt(i);
                    break;
                }
            }
        }

        public void PowerupCompleteOrder()
        {
            int countOrderComplete = 0;
            for (int i = 0; i < _foodOrder.Count; )
            {
                if(_foodOrder[i].statusOrder != STATUS.FOOD_COMPLETE && countOrderComplete <= 3)
                {
                    ++countOrderComplete;
                    foreach(FoodInfo fi in _foodOrder[i].foodResource)
                    {
                        while(fi.Amount > 0)
                        {
                            for (int j = 0; j < _foodForSpawn.Count; j++)
                            {
                                if (_foodForSpawn[j].ID.Equals(fi.id))
                                {
                                    _foodForSpawn.RemoveAt(j);
                                    break;
                                }
                            }
                            fi.Amount--;
                        }
                    }
                    _foodOrder[i].statusOrder = STATUS.FOOD_COMPLETE;
                    _foodOrder[i].haveUpdate = true;
                    UpdateSlotOrder(_foodOrder[i]);
                }
                else
                {
                    i++;
                }
            }
        }

        public void PowerupCompleteFoodLotsOfAllOrder()
        {
            for (int i = 0; i < _foodOrder.Count; i++)
            {
                if (_foodOrder[i].statusOrder != STATUS.FOOD_COMPLETE)
                {
                    FoodInfo targetFood = new FoodInfo();
                    int maxFoodResource = 0;
                    foreach (FoodInfo fi in _foodOrder[i].foodResource)
                    {
                        if(fi.Amount > maxFoodResource)
                        {
                            maxFoodResource = fi.Amount;
                            targetFood = fi;
                        }
                    }

                    while (targetFood.Amount > 0)
                    {
                        for (int j = 0; j < _foodForSpawn.Count; j++)
                        {
                            if (_foodForSpawn[j].ID.Equals(targetFood.id))
                            {
                                _foodForSpawn.RemoveAt(j);
                                break;
                            }
                        }
                        targetFood.Amount--;
                        _foodOrder[i].CompletePartProgressOrder();
                    }
                    _foodOrder[i].haveUpdate = true;
                    UpdateSlotOrder(_foodOrder[i]);
                }
            }
        }
    }
}

