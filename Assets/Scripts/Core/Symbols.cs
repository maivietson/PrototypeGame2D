using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrototypeGame2D.Core
{
    public class Symbols : MonoBehaviour
    {
        [SerializeField]
        private static string[] _listSymbol = {
            "up",
            "down",
            "left",
            "right"
        };

        public static string GetRandomSymbol()
        {
            int indexRan = Random.Range(0, _listSymbol.Length - 1);
            return _listSymbol[indexRan];
        }
    }
}

