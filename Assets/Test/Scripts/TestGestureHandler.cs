﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GestureRecognizer;
using UnityEngine.UI;
public class TestGestureHandler : MonoBehaviour
{
	public Text debugText;
	public Dropdown dropdownMin;
	public Dropdown dropdownMax;
	public Slider scoreMatch;

	public DrawDetector[] detectors;

    private void Start()
    {
		OnChangeMinMax();
	}

    public void OnChangeMinMax()
	{
		int min = dropdownMin.value + 1;
		int max = dropdownMax.value + 1;
		foreach (var detector in detectors)
		{
			detector.MinLines = min;
			detector.MaxLines = max;
			detector.ClearLines();
		}
	}



	//Gesture handler
	public void OnRecognize(RecognitionResult result)
    {
		StopAllCoroutines();
		
		if (result != RecognitionResult.Empty)
		{
			debugText.text = result.gesture.id + " " + Mathf.RoundToInt(result.score.score * 100) + "%";
			//StartCoroutine(Blink(result.gesture.id));
		}
		else
		{
			debugText.text = "NA";
		}
	}

	public void OnScoreMatchChange()
    {
		foreach (var detector in detectors) {
			detector.scoreToAccept = scoreMatch.value;
		}
	}
}
