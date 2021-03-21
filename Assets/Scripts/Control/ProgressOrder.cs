using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PrototypeGame2D.Control
{
    public class ProgressOrder : MonoBehaviour
    {
        [SerializeField] private GameObject _progress;

        private void Start()
        {
            //InitProgress();
        }

        public void InitProgress()
        {
            _progress.transform.GetChild(0).GetComponent<Image>().color = new Color32(0, 255, 13, 255);
        }

        public void UpdatePrgressOrder(float timeOrder, float timeCurrent)
        {
            float percent = timeCurrent / timeOrder;
            percent = Mathf.Clamp(percent, 0, 1);
            if (percent >= 0.75f)
            {
                _progress.transform.localScale = new Vector3(percent, 1, 1);
            }
            if (percent >= 0.25f && percent < 0.75f)
            {
                _progress.transform.GetChild(0).GetComponent<Image>().color = new Color32(255, 199, 0, 255);
                _progress.transform.localScale = new Vector3(percent, 1, 1);
            }
            if (percent >= 0 && percent < 0.25f)
            {
                _progress.transform.GetChild(0).GetComponent<Image>().color = new Color32(255, 0, 9, 255);
                _progress.transform.localScale = new Vector3(percent, 1, 1);
            }
        }
    }
}

