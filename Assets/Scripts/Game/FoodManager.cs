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
        private List<FoodOrder> _allMenuOrder;
        private List<FoodInfo> _allFoodResource;
        private int _numberMenuOrder;

        private bool _haveFoodOrder;
        private bool _refreshFoodResource;

        private int _check = 0;

        [SerializeField] private Sprite[] _imageResourceFood;
        [SerializeField] private Sprite[] _imageFoodOrder;

        private float _timeCountdown = 0.2f;

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
            _allMenuOrder = new List<FoodOrder>();

            _refreshFoodResource = false;
            _haveFoodOrder = false;

            InitMenuOrder();
        }

        // Update is called once per frame
        void Update()
        {
            if(_foodOrder.Count > 0 && !GameManager.Instance.isGameOver)
            {
                int missingOrder = 0;
                for(int i = 0; i < _foodOrder.Count; i++)
                {
                    Debug.Log("orderTime: " + _foodOrder[i].timeOrder);
                    if(_foodOrder[i].timeOrder > 0)
                    {
                        _foodOrder[i].CountDownTime(_timeCountdown * Time.deltaTime);
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

        private void InitMenuOrder()
        {
            List<FoodInfo> foodResource = new List<FoodInfo>();
            string idOrder = "sushi_ca_hoi";
            FoodInfo fi = new FoodInfo();
            List<string> symbol = new List<string>();
            symbol.Add("up");
            symbol.Add("down");
            fi.SetFoodInfo("ca_hoi", idOrder, _imageResourceFood[0], 1, symbol);
            FoodInfo fi2 = new FoodInfo();
            List<string> symbol2 = new List<string>();
            symbol2.Add("left");
            fi2.SetFoodInfo("com", idOrder, _imageResourceFood[1], 2, symbol2);
            foodResource.Add(fi);
            foodResource.Add(fi2);

            FoodOrder fo = new FoodOrder();
            fo.Name = "Sushi";
            fo.SetOrderFood(idOrder, 5.0f, 5.0f, 3.0f, _imageFoodOrder[0], foodResource);
            _allMenuOrder.Add(fo);

            _numberMenuOrder = _allMenuOrder.Count;
        }

        public FoodOrder OrderFood(string id)
        {
            FoodOrder foodOrder = new FoodOrder();
            foodOrder = _allMenuOrder.SingleOrDefault(item => item.id == id);
            AddOrder(foodOrder);
            _haveFoodOrder = true;
            FoodSpawn.Instance.StartSpawnFood();

            return foodOrder;
        }

        public void RemoveOrder(FoodOrder[] foodOrder)
        {
            foreach(FoodOrder fo in foodOrder)
            {
                for (int i = 0; i < fo.foodResource.Count; i++)
                {
                    var result = _allFoodResource.SingleOrDefault(item => item.idFoodOrder == fo.id);
                    _allFoodResource.Remove(result);
                }
            }
        }

        //public void ComplePartFoodResource(string id)
        //{
        //    FoodOrder order = _allMenuOrder.SingleOrDefault(item => item.id == id);
        //    order.CompletePartProgressOrder();
        //    OrderArea areaOrder = FindObjectOfType<OrderArea>();
        //    if (order.statusOrder == STATUS.FOOD_COMPLETE)
        //    {
        //        areaOrder.UpdateCompleteOrder(order);
        //        StartCoroutine(RemoveFoodOrder(order));
        //    }
        //    else
        //    {
        //        areaOrder.UpdateSlotOrderFood(order);
        //    }
        //}

        //private IEnumerator RemoveFoodOrder(FoodOrder foodOrder)
        //{
        //    yield return new WaitForSeconds(3.0f);
        //    var result = _allMenuOrder.SingleOrDefault(item => item.id == foodOrder.id);
        //    _allMenuOrder.Remove(result);
        //}

        public void ProgressFoodOrder(string id)
        {
            FoodOrder order = _foodOrder.SingleOrDefault(item => item.id == id);
            order.CompletePartProgressOrder();

            OrderArea areaOrder = FindObjectOfType<OrderArea>();
            areaOrder.UpdateProgressOrder(_foodOrder);
        }

        public void RemoveFoodResource(FoodInfo food)
        {
            //++_check;
            //Debug.Log("RemoveFoodResource " + _check);
            refreshFoodResource = true;

            FoodOrder r = _foodOrder.SingleOrDefault(i => i.id == food.idFoodOrder);
            r.haveUpdate = true;
            var foodInfo = r.foodResource.SingleOrDefault(i => i.id == food.id);
            if(foodInfo.Amount > 0)
                foodInfo.Amount -= 1;

            Debug.Log(foodInfo.id + " " + foodInfo.Amount);
            if (foodInfo.Amount == 0)
            {
                _allFoodResource.Remove(foodInfo);
            }

            //ComplePartFoodResource(food.idFoodOrder);
            ProgressFoodOrder(food.idFoodOrder);
        }

        public void AddOrder(FoodOrder foodOrder)
        {
            _foodOrder.Add(foodOrder);

            // sort order follow time order
            _foodOrder.OrderBy(item => item.timeOrder);

            List<FoodInfo> foods = new List<FoodInfo>();
            foods = foodOrder.foodResource;
            foreach(FoodInfo fi in foods)
            {
                _allFoodResource.Add(fi);
            }
        }
    }
}

