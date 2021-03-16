using PrototypeGame2D.Core;
using PrototypeGame2D.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrototypeGame2D.Object
{
    public class Customer : MonoBehaviour
    {
        [SerializeField] private string _name;
        [SerializeField] private float _timeOrder;
        [SerializeField] private Sprite[] _imageResourceFood;
        [SerializeField] private Sprite _imageFoodOrder;

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
            if (!_ordered)
            {
                //Debug.Log("nfsjd");
                _ordered = true;
                StartOrder();
            }
        }

        public void StartOrder()
        {
            FoodOrder order = CreateOrder();
            FoodManager.Instance.OrderFood(order);
            OrderArea areaOrder = FindObjectOfType<OrderArea>();
            areaOrder.OrderFood(order);
        }

        public FoodOrder CreateOrder()
        {
            List<FoodInfo> foodResource = new List<FoodInfo>();
            string idOrder = _name;
            FoodInfo fi = new FoodInfo();
            List<string> symbol = new List<string>();
            symbol.Add(Symbols.GetRandomSymbol());
            fi.SetFoodInfo("ca_hoi", idOrder, _imageResourceFood[0], 1, symbol);
            FoodInfo fi2 = new FoodInfo();
            List<string> symbol2 = new List<string>();
            symbol2.Add(Symbols.GetRandomSymbol());
            fi2.SetFoodInfo("com", idOrder, _imageResourceFood[1], 2, symbol2);
            foodResource.Add(fi);
            foodResource.Add(fi2);

            FoodOrder fo = new FoodOrder();
            fo.Name = _name;
            fo.SetOrderFood(idOrder, _timeOrder, 5.0f, 3.0f, _imageFoodOrder, foodResource);

            return fo;
        }
    }
}

