using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GestureRecognizer;

public class SCR_BossOverlap : MonoBehaviour, IBossInfo
{

    public Image[] symbolsBG;
    public GesturePatternDraw[] symbolsDrawing;
    private int currentActive;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            HideImage();
        }
    }


    public void SetGesture(GesturePattern[] patterns)
    {
        //throw new System.NotImplementedException();
        for (int i = 0; i < patterns.Length; i++)
        {
            symbolsDrawing[i].pattern = patterns[i];
        }
        currentActive = 0;
    }

    //Check drew gesture
    public void HandleRightGesture(GesturePattern drewPattern)
    {
        if (symbolsDrawing[currentActive].pattern.id.Equals(drewPattern.id))
        {
            HideImage();
            
        }
    }

    private void HideImage()
    {
        symbolsBG[currentActive].enabled = false;
        symbolsDrawing[currentActive].enabled = false;
        currentActive++;
    }
}
