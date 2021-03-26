using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrototypeGame2D.Core
{
    [System.Serializable]
    public class Orders
    {
        public string ID;
        public string Name;
        public string Image;
        public float TimeOrder;
        public float PriceOrder;
        public float PriceMissingOrder;
        public ResourceFood[] ResourceFoodOrder;
        public bool Semi;
    }
}

