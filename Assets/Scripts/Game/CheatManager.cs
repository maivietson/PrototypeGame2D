using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PrototypeGame2D.Game
{
    public class CheatManager : MonoBehaviour
    {
        [SerializeField] private Text _speedConveyor;
        [SerializeField] private GameObject _menuCheat;

        private bool _enableCheatSpeedConveyor;
        private bool _enableCheatMenu;

        private float SPEED_CONVEYOR_BASE = 0.5f;
        private float SPEED_CONVEYOR = 0.05f;

        private void Start()
        {
            _enableCheatSpeedConveyor = false;
            _enableCheatMenu = false;
        }

        public void SpeedConveyorClick()
        {
            _enableCheatSpeedConveyor = !_enableCheatSpeedConveyor;
            _enableCheatMenu = !_enableCheatMenu;
            _menuCheat.SetActive(_enableCheatMenu);
            _speedConveyor.gameObject.SetActive(_enableCheatSpeedConveyor);
        }

        public void CheatSpeedConveyor(float value)
        {
            _speedConveyor.text = "Speed: " + value.ToString();
            FoodManager.Instance.SpeedSpawn = value * SPEED_CONVEYOR + SPEED_CONVEYOR_BASE;
        }

        public void CheatClick()
        {
            _enableCheatMenu = !_enableCheatMenu;
            _menuCheat.SetActive(_enableCheatMenu);
        }
    }
}

