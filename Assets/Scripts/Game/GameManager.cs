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
        private int dishComplete;
        private int _idNumber;
        private int _missingOrder = Defination.LIMIT_MISSING_ORDER;
        private List<FoodOrder> listDishForOrder;

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
            CheckNumberDishComplete();

            DisplayUIMoney(money);
        }

        private void DisplayUIMoney(float money)
        {
            _money += money;
            _textMoney.text = _money.ToString();
        }

        private void CheckNumberDishComplete()
        {
            if(!PowerUpManager.Instance.IsPowerUpSlowConveyor && !PowerUpManager.Instance.PowerupCompleteAllFoodInConveyor) ++dishComplete;
            if(dishComplete == ThemesManager.Instance.LimitDifficult)
            {
                IncrementDifficult();
                IncrementLevelSpeed();
                ThemesManager.Instance.IncrementLimitDifficult(listDishForOrder.Count);
            }
        }

        private void IncrementLevelSpeed()
        {
            FoodManager.Instance.LevelConveyor += 1;
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
            listDishForOrder = new List<FoodOrder>();
            dishComplete = 0;
            _isGameOver = false;
            _idNumber = 0;

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
        }

        private void MakeFirstTime()
        {
            for(int i = 0; i < ThemesManager.Instance.StartingNumberOfDish; i++)
            {
                listDishForOrder.Add(ThemesManager.Instance.GetDishFromPool());
            }
        }    

        private void MakeFirstOrder()
        {
            for (int i = 0; i < ThemesManager.Instance.StartingNumberOfDish; i++)
            {
                OrderFood();
            }
            OrderFood(5.0f);
        }

        private void IncrementDifficult()
        {
            listDishForOrder.Add(ThemesManager.Instance.GetDishFromPool());
        }

        private void SetTextLive(int live)
        {
            _textLive.text = live.ToString();
        }

        public void OrderFood(float timeOrder = 0)
        {
            StartCoroutine(DelayOrderFood(timeOrder));
            _idNumber++;
        }

        IEnumerator DelayOrderFood(float timeOrder)
        {
            yield return new WaitForSeconds(timeOrder);
            int ranOrder = Random.Range(0, ThemesManager.Instance.ListDishPool.Count);
            FoodOrder order = new FoodOrder(ThemesManager.Instance.ListDishPool[ranOrder]);
            order.id = _idNumber.ToString();
            
            StartOrder(order);
        }

        public void StartGame()
        {
            if(ThemesManager.Instance.CreateTheme(THEME.THEME_JAPAN))
            {
                MakeFirstTime();
                MakeFirstOrder();
            }
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

