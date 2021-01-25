using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PrototypeGame2D.Game
{
    public class SceneManager : MonoBehaviour
    {
        [SerializeField] Text _textScore;
        [SerializeField] Text _textMissingSymbol;

        private string _message = string.Empty;
        private bool _isGameOver;
        private int _score = 0;
        private int _missSymbol = 0;

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
        }

        public void SendAction(string message)
        {
            _message = message;
        }

        public void AddScore()
        {
            _score += 1;
            DisplayScore();
        }

        public void AddMissingSymbol()
        {
            _missSymbol += 1;
            DisplayMissingSymbol();
        }

        private void DisplayScore()
        {
            _textScore.text = "Score: " + _score.ToString();
        }

        private void DisplayMissingSymbol()
        {
            _textMissingSymbol.text = "Miss: " + _missSymbol.ToString();
        }

        private void Update()
        {
            if(_missSymbol == 10)
            {
                _isGameOver = true;
            }
        }
    }
}
