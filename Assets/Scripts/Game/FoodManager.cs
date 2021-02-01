using PrototypeGame2D.Object;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

        [SerializeField] private Sprite[] _imageResourceFood;
        [SerializeField] private Sprite[] _imageFoodOrder;

        public bool haveFoodOrder
        {
            get { return _haveFoodOrder; }
            set { _haveFoodOrder = value; }
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

            InitMenuOrder();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void InitMenuOrder()
        {
            List<FoodInfo> foodResource = new List<FoodInfo>();
            string idOrder = "sushi_ca_hoi";
            FoodInfo fi = new FoodInfo();
            List<string> symbol = new List<string>();
            symbol.Add("up");
            symbol.Add("down");
            fi.SetFoodInfo("ca_hoi", idOrder, _imageResourceFood[0], symbol);
            FoodInfo fi2 = new FoodInfo();
            List<string> symbol2 = new List<string>();
            symbol2.Add("left");
            fi2.SetFoodInfo("com", idOrder, _imageResourceFood[1], symbol2);
            foodResource.Add(fi);
            foodResource.Add(fi2);

            FoodOrder fo = new FoodOrder();
            fo.SetOrderFood(idOrder, 5.0f, 5.0f, 3.0f, _imageFoodOrder[0], foodResource);
            _allMenuOrder.Add(fo);

            _numberMenuOrder = _allMenuOrder.Count;
        }

        public void StartOrderFood(FoodOrder[] foods)
        {
            var foodOrder = foods.Clone() as FoodOrder[];
            _foodOrder = foodOrder.ToList();
            foreach (FoodOrder fo in _foodOrder)
            {
                foreach (FoodInfo fi in fo.foodResource)
                {
                    _allFoodResource.Add(fi);
                }
            }
        }

        public void OrderFood(string id)
        {
            FoodOrder foodOrder = _allMenuOrder.SingleOrDefault(item => item.id == id);
            AddOrder(foodOrder);
            _haveFoodOrder = true;
            FoodSpawn.Instance.StartSpawnFood();
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

        public void AddOrder(FoodOrder foodOrder)
        {
            List<FoodInfo> foods = foodOrder.foodResource;
            foreach(FoodInfo fi in foods)
            {
                _allFoodResource.Add(fi);
            }
        }
    }
}

