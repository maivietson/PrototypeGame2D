using PrototypeGame2D.Control;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrototypeGame2D.Object
{
    public class OrderSlot : MonoBehaviour
    {
        private bool _isSlotEmpty;
        private FoodOrder _foodOrder;
        private string _id;
        private float _time;

        [SerializeField] TextMesh _text;
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
            transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = image;
            _isSlotEmpty = false;
        }

        public void DisplayProgress()
        {
            _progressBar.SetActive(true);
        }

        public void UpdateProgress()
        {
            _text.text = _foodOrder.Name;
            GetComponentInChildren<FoodResourceSlot>().SetupSlot(_foodOrder.foodResource);
        }

        public void UpdateProgressBar()
        {
            Debug.Log("OrderSlot: timeForOrder " + _foodOrder.TimeForOrder + " timeOrder: " + _foodOrder.timeOrder + " Name: " + _foodOrder.Name);
            GetComponentInChildren<ProgressOrder>().UpdatePrgressOrder(_foodOrder.TimeForOrder, _foodOrder.timeOrder);
        }

        public void ResetSlot()
        {
            transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = null;
            GetComponentInChildren<FoodResourceSlot>().Reset();
            _isSlotEmpty = true;
            _text.text = "";
            _progressBar.SetActive(false);
        }
    }
}

