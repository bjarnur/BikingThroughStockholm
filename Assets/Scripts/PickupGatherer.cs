using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupGatherer : MonoBehaviour {

    public List<Material> materials = new List<Material>(2);

    public delegate void OnPickupDelegate(GameObject pickup);
    public event OnPickupDelegate OnPickup;

    public float distance;
    public float delay;

    GameObject last;
    float timeCounter;

	void Start () {

	}
	
	void Update () {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, distance))
        {
            GameObject objectHit = hit.transform.gameObject;
            if (objectHit.CompareTag("Pickup"))
            {
                Debug.Log("ray hit");
                if (objectHit == last)
                {
                    //Dissolve pickp
                    objectHit.GetComponent<MeshRenderer>().material = materials[0];
                    float dissolveValue = ((timeCounter/delay) * 2) - 1.0f;
                    Debug.Log("dissolve value " + dissolveValue);
                    objectHit.GetComponent<MeshRenderer>().material
                        .SetFloat("Vector1_7D4954D9", (timeCounter - 1.0f));

                    timeCounter += Time.deltaTime;
                    //last.GetComponent<MeshRenderer>().material.color = new Color(251, 129, 8);
                    if (timeCounter >= delay)
                    {
                        if (OnPickup != null)
                            OnPickup(objectHit);
                        Destroy(objectHit);
                    }
                } else
                {
                    last = objectHit;                    
                    timeCounter = 0;
                }
            
            }
        } else
        {
            if (last != null)
            { 
                //last.GetComponent<MeshRenderer>().material.color = new Color(231, 189, 8);
                
                //Stop dissolving pickup
                last.GetComponent<MeshRenderer>().material = materials[1];
            }
            last = null;
        }
	}

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * distance);
        Gizmos.DrawSphere(transform.position + transform.forward * distance, 0.2f);
    }
}
