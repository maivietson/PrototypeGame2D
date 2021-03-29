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

        [SerializeField] ParticleSystem cardEffects;

        private void Awake()
        {
            cardEffects.Stop();
        }

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
        }

        public void SetImageOrder(Sprite image)
        {
            transform.GetChild(1).GetComponent<Image>().sprite = image;
            _isSlotEmpty = false;
        }

        public void DisplayProgress()
        {
            _progressBar.SetActive(true);
        }

        public void UpdateProgress()
        {
            _nameOrder.text = _foodOrder.Name;
            GetComponentInChildren<FoodResourceSlot>().SetupSlot(_foodOrder.foodResource);
        }

        public void UpdateProgressBar()
        {
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
            TurnOffEffect();
            UpdatPosition();
            ResetUI();
            ResetProgressBar();

            _isSlotEmpty = true;

            // Order
            GameManager.Instance.OrderFood(5.0f);
        }

        private void TurnOffEffect()
        {
            ToggleEffectSemi(false);
        }

        private void UpdatPosition()
        {
            this.transform.SetSiblingIndex(3);
        }

        private void ResetProgressBar()
        {
            GetComponentInChildren<ProgressOrder>().InitProgress();
            _progressBar.SetActive(false);
        }

        private void ResetUI()
        {
            transform.GetChild(1).GetComponent<Image>().sprite = null;
            _nameOrder.text = "";
            GetComponentInChildren<FoodResourceSlot>().Reset();
        }

        public void ToggleEffectSemi(bool semi)
        {
            if(semi)
            {
                cardEffects.Play();
            }
            else
            {
                cardEffects.Stop();
            }
        }
    }
}

