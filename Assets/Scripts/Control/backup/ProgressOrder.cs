using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrototypeGame2D.Control
{
    public class ProgressOrder : MonoBehaviour
    {
        [SerializeField] private GameObject _progressRed;
        [SerializeField] private GameObject _progressYellow;
        [SerializeField] private GameObject _progressGreen;

        private void Start()
        {
            InitProgress();
        }

        public void InitProgress()
        {
            _progressRed.SetActive(true);
            _progressYellow.SetActive(true);
            _progressGreen.SetActive(true);
        }

        public void UpdatePrgressOrder(float timeOrder, float timeCurrent)
        {
            float percent = timeCurrent / timeOrder;
            percent = Mathf.Clamp(percent, 0, 1);
            if (percent >= 0.75f)
            {
                _progressGreen.transform.localScale = new Vector3(percent, 1, 1);
            }
            if (percent >= 0.25f && percent < 0.75f)
            {
                _progressGreen.SetActive(false);
                //_progressYellow.SetActive(true);
                _progressYellow.transform.localScale = new Vector3(percent, 1, 1);
            }
            if (percent >= 0 && percent < 0.25f)
            {
                _progressYellow.SetActive(false);
                _progressGreen.SetActive(false);
                //_progressRed.SetActive(true);
                _progressRed.transform.localScale = new Vector3(percent, 1, 1);
            }
        }
    }
}

