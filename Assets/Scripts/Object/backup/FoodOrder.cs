using PrototypeGame2D.Game;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PrototypeGame2D.Core.OrderState;

namespace PrototypeGame2D.Object
{
    public class FoodOrder : MonoBehaviour
    {
        private List<FoodInfo> _foodResources;

        private float _timeOrder;
        private float _priceOrder;
        private float _priceMissingOrder;
        private string _id;
        private Sprite _image;
        private string _name;
        private float _timeForOrder;

        private int _currentStageOrder;
        private int _totalStageOrder;

        private bool _haveUpdate;

        //public enum STATUS
        //{
        //    FOOD_COMPLETE,
        //    FOOD_NOT_COMPLETE
        //}

        private STATUS _statusOrder = STATUS.FOOD_NOT_COMPLETE;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string id
        {
            get { return _id; }
            set { _id = value; }
        }

        public float timeOrder
        {
            get { return _timeOrder; }
            set { _timeOrder = value; }
        }

        public float TimeForOrder
        {
            get { return _timeForOrder; }
            set { _timeForOrder = value; }
        }

        public float priceOrder
        {
            get { return _priceOrder; }
            set { _priceOrder = value; }
        }

        public List<FoodInfo> foodResource
        {
            get { return _foodResources; }
            set { _foodResources = value; }
        }

        public float priceMissingOrder
        {
            get { return _priceMissingOrder; }
            set { _priceMissingOrder = value; }
        }

        public Sprite imageFoodOrder
        {
            get { return _image; }
            set { _image = value; }
        }

        public STATUS statusOrder
        {
            set { _statusOrder = value; }
            get { return _statusOrder; }
        }

        public bool haveUpdate
        {
            get { return _haveUpdate; }
            set { _haveUpdate = value; }
        }

        private void Start()
        {
            _currentStageOrder = 0;
            _haveUpdate = false;
            _timeOrder = -1;
        }

        public FoodOrder()
        {
        }

        public FoodOrder(FoodOrder order)
        {
            List<FoodInfo> foodResource = new List<FoodInfo>();
            foreach(FoodInfo fi in order.foodResource)
            {
                FoodInfo food = new FoodInfo(fi);
                foodResource.Add(food);
            }
            _name = order.Name;
            SetOrderFood(order.id, order.timeOrder, order.priceOrder, order.priceMissingOrder, order.imageFoodOrder, foodResource);
        }

        public void SetOrderFood(string id, float timeOrder, float priceOrder, float priceMissingOrder, Sprite image, List<FoodInfo> foods)
        {
            _id = id;
            _foodResources = foods;
            _timeOrder = timeOrder;
            _priceOrder = priceOrder;
            _priceMissingOrder = priceMissingOrder;
            _image = image;
            _timeForOrder = timeOrder;

            foreach(FoodInfo fi in _foodResources)
            {
                for(int i = 0; i < fi.Amount; ++i)
                {
                    _totalStageOrder += 1;
                }
            }
        }

        public void CountDownTime(float time)
        {
            _timeOrder = _timeOrder - time;
        }

        public void SetStatusOrder(bool isComplete)
        {
            if(isComplete)
            {
                _statusOrder = STATUS.FOOD_COMPLETE;
            }
            else
            {
                _statusOrder = STATUS.FOOD_NOT_COMPLETE;
            }
        }

        public void CompletePartProgressOrder()
        {
            //Debug.Log("_currentStageOrder: " + _currentStageOrder + " _totalStageOrder: " + _totalStageOrder);
            _currentStageOrder++;
            CheckStatusOrder();
        }

        public int TotalStageOrder
        {
            get { return _totalStageOrder; }
        }

        public int getCurrentStageOrder()
        {
            return _currentStageOrder;
        }

        public void CheckStatusOrder()
        {
            if (_currentStageOrder == _totalStageOrder)
            {
                _statusOrder = STATUS.FOOD_COMPLETE;
            }
            else
            {
                _statusOrder = STATUS.FOOD_NOT_COMPLETE;
            }
        }

        public void Check()
        {
            Debug.Log("Name Order: " + _name);
        }

    }
}

