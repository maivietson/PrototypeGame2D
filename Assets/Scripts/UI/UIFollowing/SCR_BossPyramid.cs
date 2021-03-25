using GestureRecognizer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GestureRecognizer;

public class SCR_BossPyramid : MonoBehaviour, IBossInfo
{
    public Image[] symbolsBG;
    public GesturePatternDraw[] symbolsDrawing;
    private int currentActive;

    public void SetGesture(GesturePattern[] patterns)
    {
        for (int i = 0; i < patterns.Length; i++)
        {
            symbolsDrawing[i].pattern = patterns[i];
        }
        ShowUpOrder();
        currentActive = 0;
    }

    private void ShowUpOrder()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
