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
    public Transform[] symbols;
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

    private void UpdatePosition()
    {
        for(int i = symbols.Length - 1; i > currentActive; i--)
        {
            Vector3 curr = symbols[i].localPosition;
            Vector3 prev = symbols[i-1].localPosition;
            symbols[i].localPosition = symbols[i - 1].localPosition;
        }
    }

    public void ShowImage(int idx)
    {
        if (!symbolsBG[idx].enabled)
        {
            symbolsBG[idx].enabled = true;
            symbolsDrawing[idx].enabled = true;
        }
    }

    public void HideImage()
    {
        symbolsBG[currentActive].enabled = false;
        symbolsDrawing[currentActive].enabled = false;
        UpdatePosition();
        currentActive++;
    }
}
