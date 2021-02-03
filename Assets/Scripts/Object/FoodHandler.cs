using PrototypeGame2D.Game;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PrototypeGame2D.Object
{
    public class FoodHandler : MonoBehaviour
    {
        private List<string> _symbol;

        // Start is called before the first frame update
        void Start()
        {
            _symbol = new List<string>();
            _symbol = GetComponent<FoodInfo>().SymbolKey;
        }

        // Update is called once per frame
        void Update()
        {
            if (!GameManager.Instance.isGameOver)
            {
                string message = GameManager.Instance.message;
                if (message.Length > 0)
                {
                    //Debug.Log("Action: " + message);
                    var result = _symbol.SingleOrDefault(item => item == message);
                    _symbol.Remove(result);
                    //Debug.Log("result: " + result + " === symbolSize: " + _symbol.Count);

                    if(_symbol.Count == 0)
                    {
                        //Debug.Log("Destroy");
                        FoodInfo info = GetComponent<FoodInfo>();
                        info.isCompleteSymbol = true;
                        FoodManager.Instance.RemoveFoodResource(info);
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}

