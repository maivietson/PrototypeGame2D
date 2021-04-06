using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GestureRecognizer;
using PrototypeGame2D.Core;
using PrototypeGame2D.Game;

public class SCR_BossOverlap : MonoBehaviour, IBossInfo
{
    [SerializeField] private float priceBoss;

    public SpriteRenderer[] symbolsBG;
    public SpriteRenderer[] symbolsDrawing;
    
    public Transform[] symbols;

    private int currentActive;
    // Start is called before the first frame update
    void Start()
    {
        SetGesture(symbolsDrawing);
    }

    private void Update()
    {
        if(GameManager.Instance.GetCurrentState() == STATE.STATE_FINAL_BOSS_SPAWN)
        {
            if(GameManager.Instance.message.Length > 0)
            {
                HandleRightSymbol(GameManager.Instance.message);
            }
        }
        if(currentActive == symbolsDrawing.Length)
        {
            FoodManager.Instance.HandleFood(gameObject.name);
            //GameManager.Instance.SetState(STATE.STATE_CHANGE_THEME);
        }
    }


    public void SetGesture(GesturePattern[] patterns)
    {
        throw new System.NotImplementedException();
    }
    public void SetGesture(Sprite[] symbolList)
    {
        for (int i = 0; i < symbolList.Length; i++)
        {
            ShowImage(i);
            //symbolsDrawing[i].sprite = symbolList[i];
            symbolsDrawing[i].sprite = Resources.Load<Sprite>("symbol/" + Symbols.GetRandomSymbol()); ;
        }
        currentActive = 0;
    } 

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
        if (!symbolsDrawing[idx].enabled)
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
        if(currentActive < symbolsDrawing.Length)
            ShowImage(currentActive);
    }

    public void SetGesture(SpriteRenderer[] sprites)
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i].sprite = Resources.Load<Sprite>("symbol/" + Symbols.GetRandomSymbol()); ;
        }
        currentActive = 0;
        ShowImage(currentActive);
    }

    public void HandleRightSymbol(string symbolAction)
    {
        if (symbolsDrawing[currentActive].sprite.name.Equals(symbolAction))
        {
            HideImage();
            GameManager.Instance.message = "done";
        }
    }
}
