using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingEffect : MonoBehaviour {
    public float radius;
    public float speed;

    Vector3 startingPosition;
    float timeElapsed;

	void Start () {
        startingPosition = transform.localPosition;
        timeElapsed = 0;
	}
	
	void Update () {
        timeElapsed += Time.deltaTime * speed;
        transform.localPosition = startingPosition + Mathf.Sin(timeElapsed) * Vector3.up * radius;
	}
}
