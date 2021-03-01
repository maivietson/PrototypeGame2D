using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PrototypeGame2D.Game
{
    public class CheatManager : MonoBehaviour
    {
        [SerializeField] private Text _speedConveyor;

        public void SpeedConveyorClick()
        {
            this.gameObject.SetActive(false);
            _speedConveyor.gameObject.SetActive(true);
        }

        public void CheatSpeedConveyor(float value)
        {

        }
    }
}

