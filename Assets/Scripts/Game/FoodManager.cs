using PrototypeGame2D.Object;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
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

        private List<FoodOrder> _foodOrder;
        private List<FoodInfo> _allFoodResource;
        private int _numberMenuOrder;

        private List<FoodInfoSpaw> _foodForSpawn;

        private bool _haveFoodOrder;
        private bool _refreshFoodResource;

        private FoodInfo _foodInfoTmp;
        private FoodOrder _foodOrderTmp;

        private bool _checking = false;

        private float _timeCountdown = 0.2f;

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

        public int numberMenuOrder
        {
            get { return _numberMenuOrder; }
        }

        // Start is called before the first frame update
        void Start()
        {
            _foodOrder = new List<FoodOrder>();
            _allFoodResource = new List<FoodInfo>();
            _foodForSpawn = new List<FoodInfoSpaw>();

            _refreshFoodResource = false;
            _haveFoodOrder = false;

            _foodInfoTmp = new FoodInfo();
            _foodOrderTmp = new FoodOrder();
    }

        // Update is called once per frame
        void Update()
        {
            if(_foodOrder.Count > 0 && !GameManager.Instance.isGameOver)
            {
                int missingOrder = 0;
                for(int i = 0; i < _foodOrder.Count; i++)
                {
                    if(_foodOrder[i].statusOrder == STATUS.FOOD_NOT_COMPLETE)
                    {
                        //Debug.Log("orderTime: " + _foodOrder[i].timeOrder);
                        if (_foodOrder[i].timeOrder > 0)
                        {
                            _foodOrder[i].CountDownTime(Time.deltaTime);
                        }
                        else
                        {
                            _foodOrder[i].timeOrder = 0;
                            ++missingOrder;
                            GameManager.Instance.CheckMissingOrder(missingOrder);
                        }
                    }
                }
            }
            if(!GameManager.Instance.isGameOver && _haveFoodOrder && !_checking)
            {
                _checking = true;
                FoodSpawn.Instance.StartSpawnFood();
            }
        }

        private void InitMenuOrder()
        {

        }

        public void OrderFood(FoodOrder order)
        {
            AddOrder(order);
            _haveFoodOrder = true;
        }

        public void RemoveOrder(FoodOrder foodOrder)
        {
            _foodOrder.Remove(foodOrder);
        }

        public void ProgressFoodOrder()
        {
            //FoodOrder order = _foodOrder.Where(item => item.id == id).FirstOrDefault();
            //order.CompletePartProgressOrder();

            OrderArea areaOrder = FindObjectOfType<OrderArea>();
            areaOrder.UpdateProgressOrder(_foodOrder);
        }

        public void HandleFood(string id)
        {
            Debug.Log("FoodManager: foodResource " + id + " _foodForSpawn size " + _foodForSpawn.Count);
            _refreshFoodResource = true;
            _foodForSpawn.Remove(_foodForSpawn.Where(i => i.ID == id).FirstOrDefault());
            Debug.Log("FoodManager: _foodForSpawn size " + _foodForSpawn.Count);

            float minTime = 1000;

            foreach (FoodOrder fo in _foodOrder)
            {
                if (fo.statusOrder == STATUS.FOOD_NOT_COMPLETE)
                {
                    var fi = fo.foodResource.Where(i => i.id == id).FirstOrDefault();
                    if (fo.timeOrder < minTime)
                    {
                        if(fi.Amount > 0)
                        {
                            Debug.Log("FoodManger: " + fo.id + " line 144");
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
            _foodOrderTmp.haveUpdate = true;
            if (_foodInfoTmp.Amount > 0)
            {
                _foodInfoTmp.Amount -= 1;
            }
            //if (_foodInfoTmp.Amount == 0)
            //{
            //    Debug.Log("FoodManger: " + _foodInfoTmp.id);
            //    _foodForSpawn.Remove(_foodInfoTmp);
            //}
            _foodOrderTmp.CompletePartProgressOrder();

            //ProgressFoodOrder();
            UpdateSlotOrder(_foodOrderTmp);
        }

        public void UpdateSlotOrder(FoodOrder order)
        {
            OrderArea areaOrder = FindObjectOfType<OrderArea>();
            areaOrder.UpdateProgress(order);
        }

        public void RemoveFoodResource(FoodInfo food)
        {
            //++_check;
            //Debug.Log("foodOrder: " + food.idFoodOrder);
            _refreshFoodResource = true;

            float minTime = 1000;

            foreach (FoodOrder fo in _foodOrder)
            {
                if(fo.statusOrder == STATUS.FOOD_NOT_COMPLETE)
                {
                    var fi = fo.foodResource.Where(i => i.id == food.id).FirstOrDefault();
                    //if (fi != null)
                    //{
                    if (fo.timeOrder < minTime)
                    {
                        Debug.Log("FoodManger: " + fo.id + " line 144");
                        minTime = fo.timeOrder;
                        _foodOrderTmp = fo;
                        _foodInfoTmp = fi;
                    }
                    //}
                }
            }
            Debug.Log("FoodManger: " + _foodInfoTmp.id + " Amount: " + _foodInfoTmp.Amount);
            //if(_foodOrderTmp != null && _foodInfoTmp != null)
            //{
            //Debug.Log("SON: " + _foodOrderTmp.id + " " + _foodInfoTmp.id + " line 159");
            _foodOrderTmp.haveUpdate = true;
            //_foodOrderTmp.Check();

            if (_foodInfoTmp.Amount > 0)
            {
                _foodInfoTmp.Amount -= 1;
            }
            Debug.Log("FoodManger: " + _foodInfoTmp.id + " Amount: " + _foodInfoTmp.Amount);
            if (_foodInfoTmp.Amount == 0)
            {
                Debug.Log("FoodManger: " + _foodInfoTmp.id);
                _allFoodResource.Remove(_foodInfoTmp);
                //Debug.Log("Remove: " + _foodInfoTmp.id + " and _allFoodResource: " + _allFoodResource.Count);
            }

            _foodOrderTmp.CompletePartProgressOrder();

            ProgressFoodOrder();

            //_foodOrderTmp = null;
            //_foodOrderTmp = null;
            //}

            //FoodOrder r = _foodOrder.Where(i => i.id == food.idFoodOrder).FirstOrDefault();
            //r.haveUpdate = true;
            //r.Check();
            //var foodInfo = r.foodResource.Where(i => i.id == food.id).FirstOrDefault();
            //if(foodInfo.Amount > 0)
            //    foodInfo.Amount -= 1;

            //Debug.Log(foodInfo.id + " " + foodInfo.Amount);
            //if (foodInfo.Amount == 0)
            //{
            //    _allFoodResource.Remove(foodInfo);
            //}

            //ComplePartFoodResource(food.idFoodOrder);

        }

        public void AddOrder(FoodOrder foodOrder)
        {
            _foodOrder.Add(foodOrder);

            //List<FoodInfo> foods = new List<FoodInfo>();
            //foods = foodOrder.foodResource;
            //foreach(FoodInfo fi in foods)
            //{
            //    _allFoodResource.Add(fi);
            //}

            foreach(FoodInfo fi in foodOrder.foodResource)
            {
                for(int i = 0; i < fi.Amount; i++)
                {
                    FoodInfoSpaw fis = new FoodInfoSpaw();
                    fis.SetFoodSpawn(fi.id, fi.image, fi.SymbolKey);
                    _foodForSpawn.Add(fis);
                }
            }
        }
    }
}

