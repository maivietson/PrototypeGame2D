using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        [SerializeField] private Text _text;

        private float _money;
        private int _orderMissing;

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

        public int orderMissing
        {
            get { return _orderMissing; }
            set { _orderMissing = value; }
        }

        public void CalculateMoney(float money)
        {
            _money += money;
            _text.text = "$ " + _money.ToString();
        }

        public void CheckMissingOrder(int orderMissing)
        {
            if(orderMissing == 1)
            {
                Debug.Log("GameOver");
                _isGameOver = true;
            }
        }
    }
}

