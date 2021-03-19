using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatCooking.Objects
{
    public class Dish : MonoBehaviour
    {
        private List<FoodIngredients> mFoodIngredients;

        private float mTimeOrder;
        private float mPriceOrder;
        private float mPriceMissing;

        private string mID;
        private string mName;

        private Sprite mImage;

        public void InitDishOrder(string id, string name, float timeorder, float priceorder, float pricemissing, Sprite image, List<FoodIngredients> listIngredients)
        {
            mID = id;
            mName = name;
            mPriceOrder = priceorder;
            mPriceMissing = pricemissing;
            mTimeOrder = timeorder;
            mImage = image;
            mFoodIngredients = listIngredients;
        }
    }
}

