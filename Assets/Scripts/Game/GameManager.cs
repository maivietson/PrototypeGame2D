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
        STATE_START = 0,
        STATE_PLAY = 1,
        STATE_PAUSE = 2,
        STATE_RESUME = 3,
        STATE_GAMEOVER = 4,
        STATE_FINAL_BOSS = 5,
        STATE_CHANGE_THEME = 6,
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

        private string _message = string.Empty;

        private STATE currentState;
        private THEME currentTheme;

        [SerializeField] private Text _textMoney;
        [SerializeField] private Text _textLive;
        [SerializeField] private Text _speedSpawn;
        [SerializeField] private Text _completeOrder;
        [SerializeField] TextAsset dataJson;

        private float _money;
        private int dishComplete;
        private int _idNumber;
        private int _missingOrder = Defination.LIMIT_MISSING_ORDER;
        private int _numberAppearSemi;
        private List<FoodOrder> listDishForOrder;

        private bool generateSemi;

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
                currentState = STATE.STATE_GAMEOVER;
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
            _idNumber = 0;
            _numberAppearSemi = 0;
            currentState = STATE.STATE_START;
            currentTheme = THEME.THEME_JAPAN;
            _completeOrder.text = dishComplete.ToString();
        }

        private void Update()
        {
            if(currentState == STATE.STATE_GAMEOVER)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(2);
            }

            if(currentState == STATE.STATE_PLAY)
            {
                if(_numberAppearSemi == 3)
                {
                    currentState = STATE.STATE_FINAL_BOSS;
                    _numberAppearSemi = 0;
                }
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

        //private void AppearSemiBoss()
        //{
        //    listDishForOrder.Add(ThemesManager.Instance.GetDishSemiFromPool());
        //}

        private void IncrementDifficult()
        {
            listDishForOrder.Add(ThemesManager.Instance.GetDishFromPool());
        }

        private void SetTextLive(int live)
        {
            _textLive.text = live.ToString();
        }

        public void AppearBoss()
        {
            FoodOrder order = new FoodOrder(ThemesManager.Instance.GetDishBoss());
            order.id = _idNumber.ToString();

            StartOrder(order);
        }

        public void OrderFood(float timeOrder = 0)
        {
            if(currentState == STATE.STATE_PLAY || currentState == STATE.STATE_START)
            {
                StartCoroutine(DelayOrderFood(timeOrder));
                _idNumber++;
            }
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
            if(ThemesManager.Instance.CreateTheme(currentTheme))
            {
                currentState = STATE.STATE_START;
                MakeFirstTime();
                MakeFirstOrder();
            }
        }

        public void ChangeTheme()
        {
            currentTheme += 1;
            if((int)currentTheme > 2)
            {
                currentTheme = THEME.THEME_JAPAN;
            }
            //ThemesManager.Instance.CreateTheme(currentTheme);
            //currentState = STATE.STATE_PLAY;
            StartGame();
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
                _numberAppearSemi++;
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

        public void SetState(STATE state)
        {
            currentState = state;
        }

        public THEME GetCurrentTheme()
        {
            return currentTheme;
        }

        public void PauseGame()
        {
            currentState = STATE.STATE_PAUSE;
        }
    }
}

