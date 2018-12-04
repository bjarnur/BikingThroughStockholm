using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 *  === Configuration ===
 * 
 * Burst 
 *  : Set to true to release all particles at the same time
 * numberOfParticles 
 *  : Total number of Instantiate calls that will be made 
 * systemLife 
 *  : All particles terminated after this time
 * particleLife 
 *  : How many seconds each particle will exist for
 */
public class ConfettiSystem : MonoBehaviour {

    [SerializeField] private bool burst = false;
    [SerializeField] private int numberOfParticles = 200;
    [SerializeField] private float systemLife = 5;
    [SerializeField] private float particleLife = 2;
    [SerializeField] private GameObject confettiPrefab;

    private int particleCounter = 0;
    private Vector3 maxBounds;
    private Vector3 minBounds;    
    private GameObject[] particles;
    private float[] particleLifetimes;

    void Start ()
    {
        particles = new GameObject[numberOfParticles];
        particleLifetimes = new float[numberOfParticles];

        BoxCollider bc = GetComponent<BoxCollider>();
        maxBounds = bc.bounds.max;
        minBounds = bc.bounds.min;

        if(burst)
        {
            for (int i = 0; i < numberOfParticles; i++)
            {
                float xCoord = Random.Range(minBounds.x, maxBounds.x);
                float yCoord = Random.Range(minBounds.y, maxBounds.y);
                float zCoord = Random.Range(minBounds.z, maxBounds.z);

                Instantiate(confettiPrefab, new Vector3(xCoord, yCoord, zCoord), Quaternion.identity);
            }
        }
    }
	
	
	void Update ()
    {
        systemLife -= Time.deltaTime;

        //Kill the system
        if (systemLife <= 0)
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            Destroy(gameObject);
            return;
        }

        //If burst is true all particles have already been instantiated
        if (burst) return;

        //Spawn new particles when relevant
        else if (systemLife > particleLife && particleCounter < numberOfParticles)
        {
            float xCoord = Random.Range(minBounds.x, maxBounds.x);
            float yCoord = Random.Range(minBounds.y, maxBounds.y);
            float zCoord = Random.Range(minBounds.z, maxBounds.z);
            Vector3 pos = new Vector3(xCoord, yCoord, zCoord);

            particles[particleCounter] = Instantiate(confettiPrefab, pos, Quaternion.identity);
            particles[particleCounter].transform.SetParent(transform, true);

            particleLifetimes[particleCounter++] = particleLife;
        }

        //Kill relevant particles
        for (int i = 0; i < particleCounter; i++)
        {
            particleLifetimes[i] -= Time.deltaTime;
            if (particleLifetimes[i] <= 0)
            {
                Destroy(particles[i]);
            }                
        }
    }
}
