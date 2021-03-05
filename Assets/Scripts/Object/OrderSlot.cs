using PrototypeGame2D.Control;
using PrototypeGame2D.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PrototypeGame2D.Object
{
    public class OrderSlot : MonoBehaviour
    {
        private bool _isSlotEmpty;
        private FoodOrder _foodOrder;
        private string _id;
        private float _time;

        [SerializeField] TextMesh _text;
        [SerializeField] Text _nameOrder;
        [SerializeField] GameObject _progressBar;

        public bool isSlotEmpty
        {
            get { return _isSlotEmpty; }
            set { _isSlotEmpty = value; }
        }

        public FoodOrder foodOrder
        {
            get { return _foodOrder; }
            set { _foodOrder = value; }
        }

        public string id
        {
            get { return _id; }
            set { _id = value; }
        }

        private void Start()
        {
            _isSlotEmpty = true;
            //_text = GetComponent<TextMesh>();
        }

        public void SetImageOrder(Sprite image)
        {
            //transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = image;
            transform.GetChild(1).GetComponent<Image>().sprite = image;
            _isSlotEmpty = false;
        }

        public void DisplayProgress()
        {
            _progressBar.SetActive(true);
        }

        public void UpdateProgress()
        {
            //_text.text = _foodOrder.Name;
            _nameOrder.text = _foodOrder.Name;
            GetComponentInChildren<FoodResourceSlot>().SetupSlot(_foodOrder.foodResource);
        }

        public void UpdateProgressBar()
        {
            Debug.Log("OrderSlot: timeForOrder " + _foodOrder.TimeForOrder + " timeOrder: " + _foodOrder.timeOrder + " Name: " + _foodOrder.Name);
            if(_foodOrder.statusOrder == Core.OrderState.STATUS.FOOD_NOT_COMPLETE)
            {
                GetComponentInChildren<ProgressOrder>().UpdatePrgressOrder(_foodOrder.TimeForOrder, _foodOrder.timeOrder);
            }
            if(_foodOrder.statusOrder == Core.OrderState.STATUS.FOOD_MISSING)
            {
                FoodManager.Instance.RemoveOrder(_foodOrder);
                ResetSlot();
            }
        }

        public void ResetSlot()
        {
            //transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = null;
            transform.GetChild(1).GetComponent<Image>().sprite = null;
            GetComponentInChildren<FoodResourceSlot>().Reset();
            GetComponentInChildren<ProgressOrder>().InitProgress();
            _isSlotEmpty = true;
            //_text.text = "";
            _nameOrder.text = "";
            _progressBar.SetActive(false);

            // Order
            GameManager.Instance.OrderFood(5.0f);
        }
    }
}

