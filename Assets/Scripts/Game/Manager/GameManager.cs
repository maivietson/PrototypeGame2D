using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatCooking.Game.Manager
{
    public class GameManager : MonoBehaviour
    {
        private ThemeManager mThemeManger;

        // Start is called before the first frame update
        void Start()
        {
            mThemeManger = GetComponent<ThemeManager>();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

