using GestureRecognizer;
using PrototypeGame2D.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PrototypeGame2D.Control
{
    public class GestureHandler : MonoBehaviour
    {
        [SerializeField] private Text textResult;
        [SerializeField] private GameManager gameManager;

        private DrawDetector _drawDetector;

        void Start()
        {
            _drawDetector = GetComponent<DrawDetector>() as DrawDetector;
        }

        public void OnRecognize(RecognitionResult result)
        {
            //StopAllCoroutines();
            if (result != RecognitionResult.Empty)
            {
                _drawDetector.ClearLines();
                textResult.text = result.gesture.id + "\n" + Mathf.RoundToInt(result.score.score * 100) + "%";
                if (result.score.score >= 0.8f)
                {
                    StartCoroutine(SendAction(result.gesture.id));
                    gameManager.message = result.gesture.id;
                }
                else
                {
                    gameManager.message = "";
                }
            }
            else
            {
                _drawDetector.ClearLines();
            }
        }

        IEnumerator SendAction(string id)
        {
            gameManager.message = id;
            yield return new WaitForSeconds(0.5f);
            gameManager.message = "";
        }
    }
}

