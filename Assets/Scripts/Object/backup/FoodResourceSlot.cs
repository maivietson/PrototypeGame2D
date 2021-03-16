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

        [SerializeField] private GameObject[] _listSlot;


        public void SetupSlot(List<FoodInfo> foodInfo)
        {
            _listFoodResource = foodInfo;
            for (int i = 0; i < _listFoodResource.Count; i++)
            {
                //_listSlot[i].GetComponent<SpriteRenderer>().sprite = foodInfo[i].image;
                //_listSlot[i].transform.GetChild(0).GetComponent<TextMesh>().text = foodInfo[i].Amount.ToString();
                Color tempColor = _listSlot[i].GetComponent<Image>().color;
                tempColor.a = 1.0f;
                _listSlot[i].GetComponent<Image>().color = tempColor;
                _listSlot[i].GetComponent<Image>().sprite = foodInfo[i].Icon;
                _listSlot[i].transform.GetChild(0).gameObject.SetActive(true);
                _listSlot[i].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = foodInfo[i].Amount.ToString();
            }
        }

        public void Reset()
        {
            for (int i = 0; i < _listFoodResource.Count; i++)
            {
                //_listSlot[i].GetComponent<SpriteRenderer>().sprite = null;
                //_listSlot[i].transform.GetChild(0).GetComponent<TextMesh>().text = "";
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

