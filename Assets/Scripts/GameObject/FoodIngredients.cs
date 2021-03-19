using CatCooking.Core.Foods;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatCooking.Objects
{
    public class FoodIngredients : AFoodIngredients, IActionHandle
    {
        public override void InitIngredients(string id, string order, Sprite image, Sprite icon, int amount)
        {
            _id = id;
            _idOrder = order;
            _imageIngridient = image;
            _imageIcon = icon;
            _amount = amount;

            InitIngredient();
        }

        public override void InitSymbol()
        {
            
        }

        public void MatchSymbol(string symbol)
        {
            throw new System.NotImplementedException();
        }

        public override void ShowSymbol()
        {
            throw new System.NotImplementedException();
        }

        public override void HideSymbol()
        {
            throw new System.NotImplementedException();
        }

        public override void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }
    }
}

