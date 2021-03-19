using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatCooking.Core.Foods
{
    public abstract class AFoodIngredients : MonoBehaviour
    {
        protected string _id;
        protected string _idOrder;
        protected List<string> _symbols;
        protected int _amount;

        protected Sprite _imageIngridient;
        protected Sprite _imageIcon;

        public abstract void InitIngredients(string id, string order, Sprite image, Sprite icon, int amount);
        public abstract void InitSymbol();
        public abstract void ShowSymbol();
        public abstract void HideSymbol();
        public abstract void SetActive(bool active);

        public void InitSprite()
        {
            GetComponent<SpriteRenderer>().sprite = _imageIngridient;
        }

        public void InitIngredient()
        {
            gameObject.SetActive(false);
            InitSprite();
        }
    }
}

