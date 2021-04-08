using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PrototypeGame2D.Game
{
    public class PopupManager : MonoBehaviour
    {
        [SerializeField] private GameObject settings;
        [SerializeField] private GameObject pause;
        [SerializeField] private GameObject popup;
        [SerializeField] private Text title;

        private int indexSceneHome = 0;
        private int indexSceneRestart = 1;
        
        public void OpenSettings()
        {
            OpenPopup();
            title.text = "Settings";
            settings.SetActive(true);
        }

        public void PauseGame()
        {
            Debug.Log("Pause Game");
            OpenPopup();
            title.text = "Pause";
            GameManager.Instance.LocalTimeScale = 0.0f;
            GameManager.Instance.SetState(STATE.STATE_PAUSE);
            pause.SetActive(true);
        }

        public void OpenPopup()
        {
            popup.SetActive(true);
        }

        public void ClosePopup()
        {
            if(GameManager.Instance.GetCurrentState() == STATE.STATE_PAUSE)
            {
                ResumeGame();
            }
            else
            {
                popup.SetActive(false);
            }
        }

        public void BackToHome()
        {
            ClosePopup();
            UnityEngine.SceneManagement.SceneManager.LoadScene(indexSceneHome);
        }

        public void RestartGame()
        {
            ClosePopup();
            UnityEngine.SceneManagement.SceneManager.LoadScene(indexSceneRestart);
        }

        public void ResumeGame()
        {
            GameManager.Instance.LocalTimeScale = 1.0f;
            GameManager.Instance.SetState(STATE.STATE_RESUME);
            ClosePopup();
        }
    }
}

