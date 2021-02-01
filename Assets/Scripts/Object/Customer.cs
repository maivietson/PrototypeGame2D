using PrototypeGame2D.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrototypeGame2D.Object
{
    public class Customer : MonoBehaviour
    {
        private string _id;
        public string id
        {
            get { return _id; }
            set { _id = value; }
        }

        private bool _ordered = false;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (FoodManager.Instance.numberMenuOrder > 0 && !_ordered)
            {
                _ordered = true;
                StartOrder("sushi_ca_hoi");
            }
        }

        public void StartOrder(string id)
        {
            FoodManager.Instance.OrderFood(id);
        }
    }
}

