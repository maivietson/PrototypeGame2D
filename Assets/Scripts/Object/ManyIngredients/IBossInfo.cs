using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBossInfo 
{
    public void SetGesture(GestureRecognizer.GesturePattern[] patterns);
    public void SetGesture(Sprite[] symbolList);
    public void SetGesture(SpriteRenderer[] sprites);
    public void ShowImage(int idx);
    public void HideImage();
    public void HandleRightSymbol(string symbolAction);
}
