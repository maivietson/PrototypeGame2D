using PrototypeGame2D.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrototypeGame2D
{
    public class ObstacleHandle : MonoBehaviour
    {
        private SceneManager sceneManager = null;
        private ObstacleInfo obstacleInfo;

        private string action;

        private void Start()
        {
            sceneManager = FindObjectOfType<SceneManager>() as SceneManager;
            obstacleInfo = GetComponent<ObstacleInfo>() as ObstacleInfo;
        }

        void Update()
        {
            if(sceneManager != null)
            {
                if (sceneManager.message.Length > 0)
                {
                    action = sceneManager.message;
                    if (action == obstacleInfo.id)
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}
