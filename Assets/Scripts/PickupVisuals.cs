using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Used to pass relevant values to shaders
 */ 
public class PickupVisuals : MonoBehaviour {
    public bool shouldUpdate;
    public Material pickupMaterial;

	void Start ()
    {
        shouldUpdate = true;
        pickupMaterial = GetComponent<Renderer>().material;
    }
	
	void Update ()
    {
        if (shouldUpdate)
        {
            Vector3 pos = GameObject.FindGameObjectWithTag("Player").transform.position;
            pickupMaterial.SetVector("_PlayerPos", new Vector4(pos.x, pos.y, pos.z, 1.0f));
        }
    }
}
