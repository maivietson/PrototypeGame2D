using PrototypeGame2D.Core;
using PrototypeGame2D.Game;
using PrototypeGame2D.Object;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrototypeGame2D.Control
{
    public class BezierFollow : MonoBehaviour
    {
        [SerializeField] private Transform[] routes;

        private int routeToGo;

        private float tParam;

        private Vector2 objPosition;

        private float speedIngredients;

        private bool coroutineAllowed;

        private float minScale, maxScale;

        //public float SpeedModifier
        //{
        //    get { return speedModifier; }
        //    set { speedModifier = value; }
        //}

        private void Start()
        {
            routeToGo = 0;
            tParam = 0;
            //speedModifier = 0.5f;
            coroutineAllowed = true;
            minScale = 0.4f;
            maxScale = 0.75f;
        }

        private void Update()
        {
            if(GameManager.Instance.GetCurrentState() == STATE.STATE_FINAL_BOSS_SPAWN)
            {
                speedIngredients = FoodManager.Instance.SpeedSpawnIngredintsBoss;
            }
            else
            {
                speedIngredients = (Defination.SPEED_CONVEYOR_BASE + FoodManager.Instance.LevelConveyor * Defination.SPEED_CONVEYOR);
            }

            if (coroutineAllowed)
                StartCoroutine(GoByTheRoute(routeToGo));
        }

        private IEnumerator GoByTheRoute(int routeNumber)
        {
            coroutineAllowed = false;

            Vector2 p0 = routes[routeNumber].GetChild(0).position;
            Vector2 p1 = routes[routeNumber].GetChild(1).position;
            Vector2 p2 = routes[routeNumber].GetChild(2).position;
            Vector2 p3 = routes[routeNumber].GetChild(3).position;

            Transform rootSymbol = transform.GetChild(1);

            while (tParam < 1)
            {
                tParam += Time.deltaTime * speedIngredients * GameManager.Instance.LocalTimeScale;

                objPosition = Mathf.Pow(1 - tParam, 3) * p0 +
                    3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 +
                    3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 +
                    Mathf.Pow(tParam, 3) * p3;

                transform.position = objPosition;
                if(routeNumber == 0)
                {
                    rootSymbol.gameObject.SetActive(false);
                    float sc = Mathf.Clamp(minScale + tParam * minScale, minScale, maxScale);
                    Vector3 scale = new Vector3(sc, sc);
                    transform.localScale = scale;
                }
                else if(routeNumber == 3)
                {
                    rootSymbol.gameObject.SetActive(false);
                    float sc = Mathf.Clamp(maxScale - tParam * minScale, minScale, maxScale);
                    Vector3 scale = new Vector3(sc, sc);
                    transform.localScale = scale;
                }
                else
                {
                    rootSymbol.gameObject.SetActive(true);
                }
                yield return new WaitForEndOfFrame();
            }

            tParam = 0f;

            routeToGo += 1;

            if (routeToGo > routes.Length - 1)
            {
                if(GameManager.Instance.GetCurrentState() == STATE.STATE_FINAL_BOSS_SPAWN)
                {
                    GameManager.Instance.SetState(STATE.STATE_GAMEOVER);
                    //Destroy(gameObject);
                }
                else
                {
                    Destroy(gameObject);
                }
            }
            coroutineAllowed = true;
        }
    }
}

