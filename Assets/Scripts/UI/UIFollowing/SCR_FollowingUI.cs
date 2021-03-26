using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SCR_FollowingUI : MonoBehaviour
{
    [HideInInspector]
    public RectTransform objRectTransform;
    private IBossInfo bossInfo;
    
    // Start is called before the first frame update
    void Start()
    {
        objRectTransform = GetComponent<RectTransform>();
        SetBossInfo();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ImageControl(GestureRecognizer.GesturePattern[] patterns)
    {
        SetBossInfo();
        bossInfo.SetGesture(patterns);
    }

    private void SetBossInfo()
    {
        if(bossInfo == null)
        {
            bossInfo = GetComponent<IBossInfo>();
        }
    }
}
