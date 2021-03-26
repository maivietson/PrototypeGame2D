using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarController : MonoBehaviour
{
    public Image healthBarImg;
    private Utilities.PowerUpState currentPowerUpState;
    private Utilities.PowerUpType powerUp;

    
    private void Start()
    {
        healthBarImg = GetComponent<Image>();
    }

    /// <summary>
    /// Range from 0 to 1
    /// </summary>
    /// <param name="value"></param>
    public void SetFillValue(float value)
    {
        healthBarImg.fillAmount = value;
    }


    public Utilities.PowerUpState GetCurrentState()
    {
        return currentPowerUpState;
    }
}
