using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrototypeGame2D.Object
{
    public class OrderSlot : MonoBehaviour
    {
        private bool _isSlotEmpty;

        public bool isSlotEmpty
        {
            get { return _isSlotEmpty; }
            set { _isSlotEmpty = value; }
        }

        private void Start()
        {
            _isSlotEmpty = true;
        }

        public void SetImageOrder(Sprite image)
        {
            GetComponent<SpriteRenderer>().sprite = image;
            _isSlotEmpty = false;
        }
    }
}

