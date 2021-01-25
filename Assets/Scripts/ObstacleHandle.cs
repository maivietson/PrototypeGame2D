using PrototypeGame2D.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrototypeGame2D
{
    public class ObstacleHandle : MonoBehaviour
    {
        [SerializeField] float _speedDown = 0.5f;
        private SceneManager sceneManager = null;
        private ObstacleInfo obstacleInfo;

        private bool _aLive = true;

        public float speedDown
        {
            get
            {
                return _speedDown;
            }
            set
            {
                _speedDown = value;
            }
        }

        private void Start()
        {
            sceneManager = FindObjectOfType<SceneManager>() as SceneManager;
            obstacleInfo = GetComponent<ObstacleInfo>() as ObstacleInfo;
        }

        void Update()
        {
            if(_aLive)
            {
                transform.Translate(0, -_speedDown * Time.deltaTime, 0);
                CheckGuestMatch();
            }        
        }

        private void CheckGuestMatch()
        {
            if (sceneManager != null)
            {
                if (sceneManager.message.Length > 0)
                {
                    if (sceneManager.message == obstacleInfo.id)
                    {
                        _aLive = false;
                        Destroy(gameObject);
                        sceneManager.SendAction("");
                        sceneManager.AddScore();
                    }
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log(collision);
            if(collision.tag == "groundLimit")
            {
                _aLive = false;
                Destroy(gameObject);
                sceneManager.AddMissingSymbol();
            }
        }
    }
}
