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
        public static List<string> mListSymbol;
        public static List<string> mListSymbolSemi;

        public static void LoadSymbolNormalForTheme(string nameTheme)
        {
            mListSymbol = new List<string>();
            TextAsset symbolJson = Resources.Load<TextAsset>("themes/" + nameTheme + "/dataSymbol/dataSymbol");
            SymbolsTemp symbols = JsonUtility.FromJson<SymbolsTemp>(symbolJson.text);
            foreach (string sy in symbols.AllSymbols)
            {
                mListSymbol.Add(sy);
            }
        }

        public static void LoadSymbolSemiForTheme(string nameTheme)
        {
            mListSymbolSemi = new List<string>();
            TextAsset symbolJson = Resources.Load<TextAsset>("themes/" + nameTheme + "/semi/symbolSemi/dataSymbol");
            SymbolsTemp symbols = JsonUtility.FromJson<SymbolsTemp>(symbolJson.text);
            foreach (string sy in symbols.AllSymbols)
            {
                mListSymbolSemi.Add(sy);
            }
        }

        public static void LoadSymbolForTheme(string nameTheme)
        {
            LoadSymbolNormalForTheme(nameTheme);
            LoadSymbolSemiForTheme(nameTheme);
        }

        public static string GetRandomSymbol()
        {
            int indexRan = Random.Range(0, mListSymbol.Count - 1);
            return mListSymbol[indexRan];
        }

        public static string GetRandomSymbolSemi()
        {
            int indexRan = Random.Range(0, mListSymbolSemi.Count - 1);
            return mListSymbolSemi[indexRan];
        }
    }
}

