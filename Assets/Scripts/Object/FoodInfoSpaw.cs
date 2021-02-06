using PrototypeGame2D.Game;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PrototypeGame2D.Object
{
    public class FoodInfoSpaw : MonoBehaviour
    {
        private string _id;
        private Sprite _image;
        [SerializeField] private List<string> _symbol;

        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public Sprite Image
        {
            get { return _image; }
            set { _image = value; }
        }

        public List<string> Symbol
        {
            get { return _symbol; }
            set { _symbol = value; }
        }

        public void SetFoodSpawn(string id, Sprite image, List<string> symbol)
        {
            _id = id;
            _image = image;
            _symbol = symbol;
        }

        private void Update()
        {
            if(!GameManager.Instance.isGameOver)
            {
                string message = GameManager.Instance.message;
                if(message.Length > 0)
                {
                    if(_symbol.Count > 0)
                    {
                        Debug.Log("FoodManager: message: " + message + " _symbol size: " + _symbol.Count);
                        var result = _symbol.Where(i => i.Equals(message)).FirstOrDefault();
                        if (result != null)
                        {
                            Debug.Log("FoodManager: message: " + message + " symbol: " + result + " _symbol size: " + _symbol.Count);
                            _symbol.Remove(result);
                            GameManager.Instance.message = "";
                        }
                    }
                    if(_symbol.Count == 0)
                    {
                        Debug.Log("FoodManager: _symbol size: " + _symbol.Count);
                        FoodManager.Instance.HandleFood(_id);
                        GameManager.Instance.message = "";
                    }
                    
                }
            }
        }
    }
}

