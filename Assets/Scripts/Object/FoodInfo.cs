using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrototypeGame2D.Object
{
    public class FoodInfo : MonoBehaviour
    {
        private string _id;
        private string _idFoodOrder;
        private List<string> _symbolKey;

        [SerializeField] private Sprite _image;

        public string id
        {
            get { return _id; }
            set { _id = value; }
        }

        public Sprite image
        {
            get { return _image; }
            set { _image = value; }
        }

        public string idFoodOrder
        {
            get { return _idFoodOrder; }
            set { _idFoodOrder = value; }
        }

        public List<string> SymbolKey
        {
            get { return _symbolKey; }
            set { _symbolKey = value; }
        }

        public void SetFoodInfo(string id, string idFoodOrder, Sprite image)
        {
            _id = id;
            _idFoodOrder = idFoodOrder;
            _image = image;
        }

        public void InitFood()
        {
            GetComponent<SpriteRenderer>().sprite = _image;
        }
    }
}

