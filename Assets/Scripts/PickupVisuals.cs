using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Used to pass relevant values to shaders
 */ 
public class PickupVisuals : MonoBehaviour {

    private Material pickupMaterial;

	void Start ()
    {
        pickupMaterial = GetComponent<Renderer>().material;
	}
	
	void Update ()
    {
        Vector3 pos = GameObject.FindGameObjectWithTag("Player").transform.position;	
        pickupMaterial.SetVector("_PlayerPos", new Vector4(pos.x, pos.y, pos.z, 1.0f));
    }
}
