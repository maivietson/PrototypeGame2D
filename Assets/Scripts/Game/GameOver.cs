using PrototypeGame2D.Control;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrototypeGame2D.Game
{
    public class GameOver : MonoBehaviour
    {
        [SerializeField] private Loader loader;

        public void PlayAgain()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
            //loader.LoaderScene();
        }
    }
}

