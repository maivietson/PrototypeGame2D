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
            Debug.Log("Symbols: Length: " + _listSymbol.Length);
            int indexRan = Random.Range(0, _listSymbol.Length - 1);
            Debug.Log("Symbols: Random: " + indexRan);
            return _listSymbol[indexRan];
        }
    }
}

