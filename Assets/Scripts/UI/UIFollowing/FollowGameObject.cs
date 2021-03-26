using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GestureRecognizer;

public class FollowGameObject : MonoBehaviour
{
    public Sprite[] symbolImg;
    public GameObject targetPrefab, targetMovement;
    public RectTransform canvasRect;
    public float _offset, _radius;
    public GesturePattern[] patterns;

    private GameObject targetObj;
    //private SCR_FollowingUI UIObjScript;
    private SCR_TargetObj targetObjScript;
    private IBossInfo bossInfo;

    public static FollowGameObject instance;

    private void Start()
    {
        instance = this;
        targetObj = Instantiate(targetPrefab);
        //UIFollowingObj = Instantiate(UIFollowPrefab, canvasRect);

        //UIObjScript = UIFollowingObj.GetComponent<SCR_FollowingUI>();
        //targetObjScript = targetObj.GetComponent<SCR_TargetObj>();
        //UIObjScript.ImageControl(patterns);
        bossInfo = targetObj.GetComponent<IBossInfo>();
        bossInfo.SetGesture(symbolImg);
    }
    private void Update()
    {
        //UIUtilities.UIFollowingScreenSpaceOverlayXY(targetObj, canvasRect, UIObjScript.objRectTransform, _offset);
        
    }   

 
}
