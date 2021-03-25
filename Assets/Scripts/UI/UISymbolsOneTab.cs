using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GestureRecognizer;

public class UISymbolsOneTab : MonoBehaviour
{
    public GesturePatternDraw[] symbolImages;
    public GameObject[] symbolImgObj;

    private RectTransform rectTransform;
    private GameObject target;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void RunUISymbols(GesturePattern[] patterns, GameObject food)
    {
        for (int i = 0; i < patterns.Length; i++)
        {
            symbolImages[i].pattern = patterns[i];
        }
        target = food;
    }

    public void FinishSymbol(int index)
    {
        symbolImgObj[index].SetActive(false);
    }

    private void Update()
    {
        UIUtilities.UIFollowingScreenSpaceOverlayXY(target, rectTransform, rectTransform, -1);
    }

}
