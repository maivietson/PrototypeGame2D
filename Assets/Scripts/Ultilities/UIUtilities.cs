using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class UIUtilities
{
    public static void UIFollowingScreenSpaceOverlayXY(GameObject target,RectTransform parentCanvas, RectTransform UIFollowing, float offset)
    {
        Vector3 offsetPos = new Vector3(target.transform.position.x, target.transform.position.y + offset, target.transform.position.z);

        Vector2 canvasPos;
        Vector2 screenPoint = Camera.main.WorldToScreenPoint(offsetPos);

        // Convert screen position to Canvas / RectTransform space <- leave camera null if Screen Space Overlay
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvas, screenPoint, null, out canvasPos);

        // Set
        UIFollowing.localPosition = canvasPos;
    }


}
