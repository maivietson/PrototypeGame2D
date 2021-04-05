using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrototypeGame2D.Control
{
    public class Loader : MonoBehaviour
    {
        [SerializeField] private Animator transition;

        [SerializeField] private float timeTransition = 2.0f;

        // Start is called before the first frame update
        void Start()
        {
            transition.enabled = false;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void LoaderScene()
        {
            StartCoroutine("RunAnimation");
        }

        IEnumerator RunAnimation()
        {
            transition.enabled = true;

            yield return new WaitForSeconds(timeTransition);

            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
    }
}

