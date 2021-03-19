using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatCooking.Game.Modules.LoadData
{
    [System.Serializable]
    public class FoodsInMenu
    {
        public string ID;
        public string Name;
        public string Image;
        public float TimeOrder;
        public float PriceOrder;
        public float PriceMissingOrder;
        public Ingredients[] Ingredients;
    }
}
