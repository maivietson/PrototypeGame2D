using CatCooking.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatCooking.GameObject
{
    public class FoodIngredients : AFoodIngredients, IActionHandle
    {
        public override void InitIngredients(string id, string order, Sprite image, Sprite icon, List<string> symbols, int amount)
        {
            _id = id;
            _idOrder = order;
            _imageIngridient = image;
            _imageIcon = icon;
            _symbols = symbols;
            _amount = amount;

            InitSprite();
        }

        public void MatchSymbol(string symbol)
        {
            throw new System.NotImplementedException();
        }
    }
}

