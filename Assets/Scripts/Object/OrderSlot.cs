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
            GetComponent<SpriteRenderer>().sprite = image;
            _isSlotEmpty = false;
        }

        public void UpdateProgress()
        {
            //Debug.Log(_foodOrder.foodResource.Count);
            string foodResource = string.Empty;
            foreach(FoodInfo fi in _foodOrder.foodResource)
            {
                foodResource += fi.Amount.ToString() + " " + fi.id + " ";
            }
            _text.text = foodResource;
        }

        public void ResetSlot()
        {
            GetComponent<SpriteRenderer>().sprite = null;
            _isSlotEmpty = true;
            _text.text = "";
        }
    }
}

