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
        [SerializeField] private SceneManager sceneManager;

        void Start()
        {

        }

        public void OnRecognize(RecognitionResult result)
        {
            StopAllCoroutines();
            if (result != RecognitionResult.Empty)
            {
                textResult.text = result.gesture.id + "\n" + Mathf.RoundToInt(result.score.score * 100) + "%";
                if (result.score.score >= 0.8f)
                {
                    StartCoroutine(DestroyObstacle(result.gesture.id));
                }
            }
        }

        IEnumerator DestroyObstacle(string id)
        {
            sceneManager.SendAction(id);
            yield return new WaitForSeconds(0.5f);
        }
    }
}

