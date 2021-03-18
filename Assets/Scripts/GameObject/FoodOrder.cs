using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatCooking.GameObject
{
    public class FoodOrder : MonoBehaviour
    {
        private List<FoodIngredients> mFoodIngredients;

        private float mTimeOrder;
        private float mPriceOrder;
        private float mPriceMissing;

        private string mID;
        private string mName;

        private Sprite mImage;

        public void InitFoodOrder(string id, float timeorder, float priceorder, float pricemissing, Sprite image, List<FoodIngredients> listIngredients)
        {
            mID = id;
            mPriceOrder = priceorder;
            mPriceMissing = pricemissing;
            mTimeOrder = timeorder;
            mImage = image;
            mFoodIngredients = listIngredients;
        }

    }
}

