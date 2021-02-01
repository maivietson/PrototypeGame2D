using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrototypeGame2D.Object
{
    public class FoodHandler : MonoBehaviour
    {
        private List<string> _symbol;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if(GameManager.Instance.message.Length > 0 && !GameManager.Instance.isGameOver)
            {

            }
        }
    }
}

