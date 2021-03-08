using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GestureRecognizer;
using UnityEngine.EventSystems;
public class TestManager : MonoBehaviour
{
    public GameObject showGesturePanel, scollObj, contentObj;
    public GameObject showLinePrefabs;


    private Rect scrollRect;
    private List<GameObject> lineImgs;
    private TestGestureHandler handler;
    // Start is called before the first frame update
    void Start()
    {
        handler = GetComponent<TestGestureHandler>();
        scrollRect = showGesturePanel.GetComponent<RectTransform>().rect;
        lineImgs = new List<GameObject>();
        foreach (var item in handler.recognizer.patterns)
        {
            GameObject tmp = Instantiate(showLinePrefabs);
            tmp.GetComponent<GesturePatternDraw>().pattern = item;
            tmp.transform.parent = contentObj.transform;
            lineImgs.Add(tmp);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region UI_Function
    public void ShowHidePanel()
    {
        if (showGesturePanel.activeSelf)
        {
            showGesturePanel.SetActive(false);
            EventSystem.current.currentSelectedGameObject.GetComponentsInChildren<Text>()[0].text = "Show Panel";
        }
        else
        {
            showGesturePanel.SetActive(true);
            EventSystem.current.currentSelectedGameObject.GetComponentsInChildren<Text>()[0].text = "Hide Panel";
        }
    }

    #endregion
}
