using GestureRecognizer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GestureHandler : MonoBehaviour
{
    [SerializeField] private Text textResult;
    [SerializeField] GameObject referenceRoot;

    ObstacleInfo references;

    void Start()
    {
        references = referenceRoot.GetComponentInChildren<ObstacleInfo>();
        Debug.Log(references.id);
    }

    void ShowAll()
    {
        references.gameObject.SetActive(true);
    }

    public void OnRecognize(RecognitionResult result)
    {
        StopAllCoroutines();
        if(result != RecognitionResult.Empty)
        {
            textResult.text = result.gesture.id + "\n" + Mathf.RoundToInt(result.score.score * 100) + "%";
            StartCoroutine(Blink(result.gesture.id));
        }
    }

    IEnumerator Blink(string id)
    {
        if(references.id == id && referenceRoot != null)
        {
            Destroy(referenceRoot);
            yield return new WaitForSeconds(1.0f);
        }
    }
}
