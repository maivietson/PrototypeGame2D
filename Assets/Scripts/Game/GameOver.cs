using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrototypeGame2D.Game
{
    public class GameOver : MonoBehaviour
    {
        public void PlayAgain()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
    }
}

