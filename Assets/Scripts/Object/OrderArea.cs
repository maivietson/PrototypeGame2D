using PrototypeGame2D.Game;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace PrototypeGame2D.Object
{
    public class OrderArea : MonoBehaviour
    {
        [SerializeField] private GameObject[] _slotOrder;

        public void OrderFood(FoodOrder foodOrder)
        {
            for(int i = 0; i < _slotOrder.Length; i++)
            {
                OrderSlot os = _slotOrder[i].GetComponent<OrderSlot>();
                if(os.isSlotEmpty)
                {
                    os.SetImageOrder(foodOrder.imageFoodOrder);
                    os.id = foodOrder.id;
                    os.foodOrder = foodOrder;
                    os.UpdateProgress();
                    break;
                }
            }
        }

        public void UpdateProgressOrder(List<FoodOrder> allFoodOrder)
        {
            var result = allFoodOrder.SingleOrDefault(item => item.haveUpdate == true);
            for (int i = 0; i < _slotOrder.Length; ++i)
            {
                OrderSlot os = _slotOrder[i].GetComponent<OrderSlot>();
                if (!os.isSlotEmpty)
                {
                    if (result.id == os.id)
                    {
                        if(result.statusOrder == Core.OrderState.STATUS.FOOD_COMPLETE)
                        {
                            GameManager.Instance.CalculateMoney(result.priceOrder);
                            os.ResetSlot();
                        }
                        else
                        {
                            os.foodOrder = result;
                            os.UpdateProgress();
                            result.haveUpdate = false;
                        }
                    }
                }
            }
        }
    }
}

