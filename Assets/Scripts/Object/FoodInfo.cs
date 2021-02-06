using System;
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
        private bool _isCompleteSymbol;
        private int _amount;

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

        public bool isCompleteSymbol
        {
            get { return _isCompleteSymbol; }
            set { _isCompleteSymbol = value; }
        }

        public int Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        public void SetFoodInfo(string id, string idFoodOrder, Sprite image, int amount, List<string> symbol)
        {
            _id = id;
            _idFoodOrder = idFoodOrder;
            _image = image;
            _symbolKey = symbol;
            _amount = amount;
            _isCompleteSymbol = false;
        }

        public void InitFood()
        {
            GetComponent<SpriteRenderer>().sprite = _image;
        }

        public void Clone(FoodInfo foodInfo)
        {
            this._id = foodInfo.id;
            this._idFoodOrder = foodInfo.idFoodOrder;
            this._image = foodInfo.image;
            this._symbolKey = foodInfo.SymbolKey;
            this._amount = foodInfo.Amount;
            _isCompleteSymbol = false;
        }
    }
}

