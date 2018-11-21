using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RythmTracker : MonoBehaviour
{

    public Image ProgressBar;
    private float progress;

	// Use this for initialization
	void Start ()
    {
        progress = 0.6f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        progress = Mathf.Max(progress - 0.1f * Time.deltaTime, 0f);
        if (Input.GetKeyDown("space"))
        {
            progress = Mathf.Min(progress + 0.1f, 1.0f);
        }

        ProgressBar.fillAmount = progress;
	}
}
