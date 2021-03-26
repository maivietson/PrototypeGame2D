using GestureRecognizer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GestureRecognizer;

public class SCR_BossPyramid : MonoBehaviour, IBossInfo
{
   // public Sprite[] _symbolList;
    public SpriteRenderer[] symbolsBG;
    public SpriteRenderer[] symbolsDrawing;
    private int currentActive;

   
    public void SetGesture(Sprite[] symbolList)
    {
        for (int i = 0; i < symbolList.Length; i++)
        {
            ShowImage(i);
            symbolsDrawing[i].sprite = symbolList[i];
        }
        currentActive = 0;
    }

    public void SetGesture(GesturePattern[] patterns)
    {
       
        //for (int i = 0; i < patterns.Length; i++)
        //{
        //    //symbolsDrawing[i].pattern = patterns[i];
        //}
        //ShowUpOrder();
        //currentActive = 0;

        throw new System.NotImplementedException();
    }

    private void ShowUpOrder()
    {

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
