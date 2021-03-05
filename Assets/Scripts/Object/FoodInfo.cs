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
        [SerializeField] private Sprite _icon;

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

        public Sprite Icon
        {
            get { return _icon; }
            set { _icon = value; }
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

        public FoodInfo()
        {

        }

        public FoodInfo(FoodInfo food)
        {
            SetFoodInfo(food.id, food.idFoodOrder, food.image, food.Icon, food.Amount, food.SymbolKey);
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

        public void SetFoodInfo(string id, string idFoodOrder, Sprite image, Sprite icon, int amount, List<string> symbol)
        {
            _icon = icon;
            SetFoodInfo(id, idFoodOrder, image, amount, symbol);
        }

        public void InitFood()
        {
            GetComponent<SpriteRenderer>().sprite = _image;
        }
    }
}

