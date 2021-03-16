using PrototypeGame2D.Game;
using System;
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
                    os.DisplayProgress();
                    os.UpdateProgress();
                    break;
                }
            }
        }

        //public void UpdateProgressOrder(List<FoodOrder> allFoodOrder)
        //{
        //    var result = allFoodOrder.Where(item => item.haveUpdate == true && item.statusOrder == Core.OrderState.STATUS.FOOD_NOT_COMPLETE).FirstOrDefault();
        //    Debug.Log("OrderArea: " + result.id + " have changed");
        //    for (int i = 0; i < _slotOrder.Length; ++i)
        //    {
        //        OrderSlot os = _slotOrder[i].GetComponent<OrderSlot>();
        //        if (!os.isSlotEmpty)
        //        {
        //            if (result.id == os.id)
        //            {
        //                if(result.statusOrder == Core.OrderState.STATUS.FOOD_COMPLETE)
        //                {
        //                    GameManager.Instance.CalculateMoney(result.priceOrder);
        //                    FoodManager.Instance.RemoveOrder(result);
        //                    //allFoodOrder.Remove(result);
        //                    os.ResetSlot();
        //                }
        //                else
        //                {
        //                    Debug.Log("OrderArea: " + result.id);
        //                    os.foodOrder = result;
        //                    os.UpdateProgress();
        //                }
        //                result.haveUpdate = false;
        //            }
        //        }
        //    }
        //}

        public void UpdateProgress(FoodOrder order)
        {
            for(int i = 0; i < _slotOrder.Length; ++i)
            {
                OrderSlot os = _slotOrder[i].GetComponent<OrderSlot>();
                if (!os.isSlotEmpty)
                {
                    if (order.id == os.id && order.haveUpdate)
                    {
                        if (order.statusOrder == Core.OrderState.STATUS.FOOD_COMPLETE)
                        {
                            GameManager.Instance.CalculateMoney(order.priceOrder);
                            FoodManager.Instance.CompleteOrder(order);
                            os.ResetSlot();
                            UpdatePositionSlot();
                        }
                        else
                        {
                            Debug.Log("OrderArea: " + order.id);
                            os.foodOrder = order;
                            os.UpdateProgress();
                            order.haveUpdate = false;
                            break;
                        }
                        order.haveUpdate = false;
                    }
                }
            }
        }

        private void UpdatePositionSlot()
        {
            for (int i = 0; i < _slotOrder.Length; ++i)
            {
                OrderSlot os = _slotOrder[i].GetComponent<OrderSlot>();
                if (os.isSlotEmpty)
                {
                    //Destroy(_slotOrder[i]);
                }
            }
        }

        public void UpdateProgressBar()
        {
            for (int i = 0; i < _slotOrder.Length; ++i)
            {
                OrderSlot os = _slotOrder[i].GetComponent<OrderSlot>();
                if (!os.isSlotEmpty)
                {
                    os.UpdateProgressBar();
                }
            }
        }
    }
}

