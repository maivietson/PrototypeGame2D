using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using PrototypeGame2D.Object;
using PrototypeGame2D.Core;

namespace PrototypeGame2D.Game
{
    public enum STATE
    {
        STATE_PLAY = 0,
        STATE_PAUSE = 1,
        STATE_GAMEOVER = 2
    }
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

        private STATE currentState;

        [SerializeField] private Text _textMoney;
        [SerializeField] private Text _textLive;
        [SerializeField] private Text _speedSpawn;
        [SerializeField] private Text _completeOrder;
        [SerializeField] TextAsset dataJson;

        private float _money;
        private int dishComplete;
        private int _idNumber;
        private int _missingOrder = Defination.LIMIT_MISSING_ORDER;
        private List<FoodOrder> listDishForOrder;

        private bool generateSemi;

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
            UpdateUICompletDish();
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
            if(dishComplete % 2 == 0)
            {
                generateSemi = true;
            }
        }

        private void IncrementLevelSpeed()
        {
            FoodManager.Instance.LevelConveyor += 1;
            UpdateUISpeedSpawn();
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
            InitGame();

            UpdateUICompletDish();
            UpdateUISpeedSpawn();

            StartGame();
            SetTextLive(Defination.LIMIT_MISSING_ORDER);
        }

        private void InitGame()
        {
            listDishForOrder = new List<FoodOrder>();
            dishComplete = 0;
            _isGameOver = false;
            _idNumber = 0;
            currentState = STATE.STATE_PLAY;
            _completeOrder.text = dishComplete.ToString();
        }

        private void Update()
        {
            if(currentState == STATE.STATE_GAMEOVER)
            {
                //_isGameOver = false;
                UnityEngine.SceneManagement.SceneManager.LoadScene(2);
            }
        }

        private void UpdateUISpeedSpawn()
        {
            _speedSpawn.text = FoodManager.Instance.LevelConveyor.ToString();
        }

        private void UpdateUICompletDish()
        {
            _completeOrder.text = dishComplete.ToString();
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

        private void AppearSemiBoss()
        {
            listDishForOrder.Add(ThemesManager.Instance.GetDishSemiFromPool());
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
            FoodOrder order = new FoodOrder(RandomNormalOrSemi());
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

        private FoodOrder RandomNormalOrSemi()
        {
            int random = Random.Range(0, 9);
            if(random >= 2 && generateSemi)
            {
                generateSemi = false;
                return ThemesManager.Instance.GetDishSemiFromPool();
            }
            else
            {
                int ranOrder = Random.Range(0, listDishForOrder.Count);
                return listDishForOrder[ranOrder];
            }
        }

        public STATE GetCurrentState()
        {
            return currentState;
        }
    }
}

