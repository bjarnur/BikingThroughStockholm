using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour {

    
    private float timeSoFar;
    private Text textField;

	void Start ()
    {
        timeSoFar = 0.0f;
        textField = GetComponent<Text>();
	}
	
	
	void Update ()
    {
        timeSoFar += Time.deltaTime;
        string minutes = Mathf.Floor(timeSoFar / 60).ToString("00");
        string seconds = (timeSoFar % 60).ToString("00");       
        textField.text = minutes + ":" + seconds;
    }
}
