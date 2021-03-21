using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrototypeGame2D.Core
{
    [System.Serializable]
    public class SymbolsTemp
    {
        public string[] AllSymbols;
    }

    public class Symbols : MonoBehaviour
    {
        [SerializeField]
        public static List<string> mListSymbol;

        public static void LoadSymbolForTheme(string nameTheme)
        {
            TextAsset symbolJson = Resources.Load<TextAsset>("themes/" + nameTheme + "/dataSymbol/dataSymbol");
            SymbolsTemp symbols = JsonUtility.FromJson<SymbolsTemp>(symbolJson.text);
            foreach (string sy in symbols.AllSymbols)
            {
                mListSymbol.Add(sy);
            }
        }

        public static string GetRandomSymbol()
        {
            int indexRan = Random.Range(0, mListSymbol.Count - 1);
            return mListSymbol[indexRan];
        }
    }
}

