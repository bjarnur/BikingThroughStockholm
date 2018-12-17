using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {
    public float degreesPerSecond;
    public Vector3 axis;
	
	void Update () {
        transform.Rotate(axis, degreesPerSecond * Time.deltaTime);
	}
}
