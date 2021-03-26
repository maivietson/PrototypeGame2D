using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GestureRecognizer;

public class SCR_BossOverlap : MonoBehaviour, IBossInfo
{
    //public Sprite[] _symbolList;
    public SpriteRenderer[] symbolsBG;
    public SpriteRenderer[] symbolsDrawing;
    private int currentActive;
    // Start is called before the first frame update
    void Start()
    {
        //SetGesture(_symbolList);
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
        throw new System.NotImplementedException();
        //for (int i = 0; i < patterns.Length; i++)
        //{
        //    //symbolsDrawing[i].pattern = patterns[i];
        //}
        //currentActive = 0;
    }
    public void SetGesture(Sprite[] symbolList)
    {
        //throw new System.NotImplementedException();
        for (int i = 0; i < symbolList.Length; i++)
        {
            ShowImage(i);
            symbolsDrawing[i].sprite = symbolList[i];
        }
        currentActive = 0;
    } 
   

    //Check drew gesture
    //public void HandleRightGesture(GesturePattern drewPattern)
    //{
    //    if (symbolsDrawing[currentActive].pattern.id.Equals(drewPattern.id))
    //    {
    //        HideImage();

    //    }
    //}

    private void HideImage()
    {
        symbolsBG[currentActive].enabled = false;
        symbolsDrawing[currentActive].enabled = false;
        currentActive++;
    }
    private void ShowImage(int idx)
    {
        if (!symbolsBG[idx].enabled)
        {
            symbolsBG[idx].enabled = true;
            symbolsDrawing[idx].enabled = true;
        }
    }
}
