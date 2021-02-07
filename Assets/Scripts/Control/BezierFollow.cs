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

        private float speedModifier;

        private bool coroutineAllowed;

        private float minScale, maxScale;

        private void Start()
        {
            routeToGo = 0;
            tParam = 0;
            speedModifier = 0.5f;
            coroutineAllowed = true;
            minScale = 0.4f;
            maxScale = 0.75f;
        }

        private void Update()
        {
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
                tParam += Time.deltaTime * speedModifier;

                objPosition = Mathf.Pow(1 - tParam, 3) * p0 +
                    3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 +
                    3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 +
                    Mathf.Pow(tParam, 3) * p3;

                transform.position = objPosition;
                if(routeNumber == 0)
                {
                    rootSymbol.gameObject.SetActive(false);
                    float sc = Mathf.Clamp(minScale + tParam, minScale, maxScale);
                    Vector3 scale = new Vector3(sc, sc);
                    transform.localScale = scale;
                }
                if(routeNumber == 2)
                {
                    rootSymbol.gameObject.SetActive(false);
                    float sc = Mathf.Clamp(maxScale - tParam * minScale, minScale, maxScale);
                    Vector3 scale = new Vector3(sc, sc);
                    transform.localScale = scale;
                }
                if(routeNumber == 1)
                {
                    rootSymbol.gameObject.SetActive(true);
                }
                yield return new WaitForEndOfFrame();
            }

            tParam = 0f;

            routeToGo += 1;

            if (routeToGo > routes.Length - 1)
            {
                //FoodSpawn.Instance.foodDestroied.Add(gameObject.GetComponent<FoodInfo>().id);
                Destroy(gameObject);
                //routeToGo = 0;
            }
            coroutineAllowed = true;
        }
    }
}

