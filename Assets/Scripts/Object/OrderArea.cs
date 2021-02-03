using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrototypeGame2D.Object
{
    public class OrderArea : MonoBehaviour
    {
        [SerializeField] private GameObject[] _slotOrder;

        public void OrderFood(Sprite image)
        {
            for(int i = 0; i < _slotOrder.Length; i++)
            {
                OrderSlot os = _slotOrder[i].GetComponent<OrderSlot>();
                if(os.isSlotEmpty)
                {
                    os.SetImageOrder(image);
                    break;
                }
            }
        }
    }
}

