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
                if(ThemesManager.Instance.CompleteLoadTheme && _limitOrder < 3)
                {
                    OrderFood();
                    _limitOrder++;
                }
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
            int ranOrder = Random.Range(0, ThemesManager.Instance.ListDishMenu.Count);
            FoodOrder order = new FoodOrder(ThemesManager.Instance.ListDishMenu[ranOrder]);
            order.id = _numberOrder.ToString();
            StartOrder(order);
        }

        public void StartGame()
        {
            ThemesManager.Instance.CreateTheme(THEME.THEME_JAPAN);
            //_listMenuInRes = ThemesManager.Instance.ListDishMenu;
        }

        public void ChangeTheme(THEME theme)
        {
            ThemesManager.Instance.CreateTheme(theme);
        }

        public void StartOrder(FoodOrder order)
        {
            FoodManager.Instance.OrderFood(order);
            OrderArea areaOrder = FindObjectOfType<OrderArea>();
            areaOrder.OrderFood(order);
        }

        public void PowerupAddLive()
        {
            if(_missingOrder < Defination.LIMIT_MISSING_ORDER)
            {
                _missingOrder++;
                SetTextLive(_missingOrder);
            }
        }
    }
}

