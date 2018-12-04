using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfettiSystem : MonoBehaviour {

    [SerializeField] private int numberOfParticles;

    [SerializeField] private GameObject confettiPrefab;

    private Vector3 maxBounds;
    private Vector3 minBounds;

    void Start ()
    {
        BoxCollider bc = GetComponent<BoxCollider>();
        maxBounds = bc.bounds.max;
        minBounds = bc.bounds.min;

        for(int i = 0; i < numberOfParticles; i++)
        {
            float xCoord = Random.Range(minBounds.x, maxBounds.x);
            float yCoord = Random.Range(minBounds.y, maxBounds.y);
            float zCoord = Random.Range(minBounds.z, maxBounds.z);

            Instantiate(confettiPrefab, new Vector3(xCoord, yCoord, zCoord), Quaternion.identity);
        }
    }
	
	
	void Update ()
    {
		
	}
}
