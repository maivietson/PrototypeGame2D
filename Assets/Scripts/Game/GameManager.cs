using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

