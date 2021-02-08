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
                if (message.Length > 0 && GetComponent<FoodInfo>().SymbolKey.Count > 0)
                {
                    GameManager.Instance.message = "";
                    Debug.Log("FoodManger: Action: " + message);
                    foreach (string it in GetComponent<FoodInfo>().SymbolKey)
                    {
                        if (it == message)
                        {
                            GetComponent<FoodInfo>().SymbolKey.Remove(it);
                            Debug.Log("FoodManger: result: " + it + " === symbolSize: " + _symbol.Count);
                            break;
                        }
                    }
                    //Debug.Log("FoodManger: Action: " + message);
                    //var result = _symbol.Where(item => item == message).FirstOrDefault();
                    //if (result.Length > 0)
                    //{
                    //    _symbol.Remove(result);
                    //    GameManager.Instance.message = "";
                    //    Debug.Log("FoodHandle: result: " + result + " === symbolSize: " + _symbol.Count);
                    //}
                    //Debug.Log("result: " + result + " === symbolSize: " + _symbol.Count);

                    if (GetComponent<FoodInfo>().SymbolKey.Count == 0)
                    {
                        //Debug.Log("Destroy");
                        FoodInfo info = GetComponent<FoodInfo>();
                        Debug.Log("FoodManger: Food complete: " + info.id + " of " + info.idFoodOrder);
                        //FoodManager.Instance.RemoveFoodResource(info);
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}

