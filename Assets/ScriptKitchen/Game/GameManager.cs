using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton class: GameManager
    public static GameManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    #endregion

    private bool _isGameOver;
    private string _message = string.Empty;

    public bool isGameOver
    {
        get
        {
            return _isGameOver;
        }
        set
        {
            _isGameOver = value;
        }
    }

    public string message
    {
        get { return _message; }
        set { _message = value; }
    }
}
