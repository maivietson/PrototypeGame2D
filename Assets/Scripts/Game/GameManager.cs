using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using PrototypeGame2D.Object;
using PrototypeGame2D.Core;

namespace PrototypeGame2D.Game
{
    public class GameManager : MonoBehaviour
    {
        #region Singleton class: GameManager
        public static GameManager Instance;

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }

        }
        #endregion

        private bool _isGameOver;
        private string _message = string.Empty;

        [SerializeField] private Text _textMoney;
        [SerializeField] private Text _textLive;
        [SerializeField] TextAsset dataJson;

        private float _money;
        private bool _completeLoad;
        private int _limitOrder;
        private int _numberOrder;
        private int _missingOrder = Defination.LIMIT_MISSING_ORDER;
        private List<FoodOrder> _listMenuInRes;

        public bool isGameOver
        {
            get
            {
                return _isGameOver;
            }
            set
            {
                _isGameOver = value;
            }
        }

        public string message
        {
            get { return _message; }
            set { _message = value; }
        }

        public float money
        {
            get { return _money; }
            set { _money = value; }
        }

        public void CalculateMoney(float money)
        {
            _money += money;
            _textMoney.text = _money.ToString();
        }

        public void MissingOrder()
        {
            --_missingOrder;
            SetTextLive(_missingOrder);

            if(_missingOrder == 0)
            {
                _isGameOver = true;
            }
        }

        private void Start()
        {
            _listMenuInRes = new List<FoodOrder>();
            _completeLoad = false;
            _limitOrder = 0;
            _numberOrder = 0;
            _isGameOver = false;

            StartGame();
            SetTextLive(Defination.LIMIT_MISSING_ORDER);
        }

        private void Update()
        {
            if(_isGameOver)
            {
                //_isGameOver = false;
                UnityEngine.SceneManagement.SceneManager.LoadScene(2);
            }
            else
            {
                if(_completeLoad && _limitOrder < 3)
                {
                    OrderFood();
                    _limitOrder++;
                }

                //if (FoodManager.Instance.GetNumberOrder() == 0)
                //{
                //    Debug.Log("GameOver");
                //    _isGameOver = true;
                //}
            }
        }

        private void SetTextLive(int live)
        {
            _textLive.text = live.ToString();
        }

        public void OrderFood(float timeOrder = 0)
        {
            StartCoroutine(DelayOrderFood(timeOrder));
        }

        IEnumerator DelayOrderFood(float timeOrder)
        {
            yield return new WaitForSeconds(timeOrder);
            ++_numberOrder;
            int ranOrder = Random.Range(0, _listMenuInRes.Count);
            FoodOrder order = new FoodOrder(_listMenuInRes[ranOrder]);
            order.id = _numberOrder.ToString();
            StartOrder(order);
        }

        public void StartGame()
        {
            Menus menusFood = JsonUtility.FromJson<Menus>(dataJson.text);
            foreach(Orders od in menusFood.OrdersFood)
            {
                List<FoodInfo> foodResource = new List<FoodInfo>();
                foreach(ResourceFood rf in od.ResourceFoodOrder)
                {
                    FoodInfo fi = new FoodInfo();
                    List<string> symbol = new List<string>();
                    symbol.Add(Symbols.GetRandomSymbol());
                    Sprite foodSprite = Resources.Load<Sprite>("foodSprite/" + rf.Image);
                    fi.SetFoodInfo(rf.ID, od.Name, foodSprite, rf.Amount, symbol);
                    foodResource.Add(fi);
                }

                FoodOrder fo = new FoodOrder();
                Sprite orderSprite = Resources.Load<Sprite>("orderSprite/" + od.Image);
                fo.Name = od.Name;
                fo.SetOrderFood(od.Name, od.TimeOrder, od.PriceOrder, od.PriceMissingOrder, orderSprite, foodResource);

                _listMenuInRes.Add(fo);
            }
            _completeLoad = true;
        }

        public void StartOrder(FoodOrder order)
        {
            FoodManager.Instance.OrderFood(order);
            OrderArea areaOrder = FindObjectOfType<OrderArea>();
            areaOrder.OrderFood(order);
        }

        public void AddLive()
        {
            if(_missingOrder < Defination.LIMIT_MISSING_ORDER)
            {
                _missingOrder++;
                SetTextLive(_missingOrder);
            }
        }
    }
}

