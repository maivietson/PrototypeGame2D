using PrototypeGame2D.Object;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PrototypeGame2D.Object
{
    public class FoodResourceSlot : MonoBehaviour
    {
        private List<FoodInfo> _listFoodResource;

        [SerializeField] private ParticleSystem[] _listEffect;

        [SerializeField] private GameObject[] _listSlot;

        private void Awake()
        {
            for (int i = 0; i < _listSlot.Length; i++)
            {
                //_listEffect[i] = _listSlot[i].transform.GetComponentInChildren<ParticleSystem>();
                _listEffect[i].Stop();
            }
        }

        public void SetupSlot(List<FoodInfo> foodInfo)
        {
            //_listFoodResource = foodInfo;
            for (int i = 0; i < foodInfo.Count; i++)
            {
                Color tempColor = _listSlot[i].GetComponent<Image>().color;
                tempColor.a = 1.0f;
                _listSlot[i].GetComponent<Image>().color = tempColor;
                _listSlot[i].GetComponent<Image>().sprite = foodInfo[i].Icon;
                _listSlot[i].transform.GetChild(0).gameObject.SetActive(true);
                _listSlot[i].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = foodInfo[i].Amount.ToString();
                if (foodInfo[i].HaveUpdate)
                {
                    _listEffect[i].Play();
                    foodInfo[i].HaveUpdate = false;
                }
            }
        }

        public void Reset()
        {
            for (int i = 0; i < _listSlot.Length; i++)
            {
                Color tempColor = _listSlot[i].GetComponent<Image>().color;
                tempColor.a = 0.0f;
                _listSlot[i].GetComponent<Image>().color = tempColor;
                _listSlot[i].GetComponent<Image>().sprite = null;
                _listSlot[i].transform.GetChild(0).gameObject.SetActive(false);
                _listSlot[i].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "";
            }
        }
    }
}

