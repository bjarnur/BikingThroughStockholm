using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfettiController : MonoBehaviour {

    private Vector3 linearVelocity;
    private Vector3 angularVelicoty;
    

	void Start ()
    {
        initialize_particle();
        linearVelocity = InitializeLinearVelocity();
        angularVelicoty = InitializeAngularVelocity();

        BoxCollider bc = GetComponent<BoxCollider>();
        Vector3 maxBounds = bc.bounds.max;
        Vector3 minBounds = bc.bounds.min;
    }
	
	void Update ()
    {
        transform.position += linearVelocity * Time.deltaTime;
        transform.Rotate(angularVelicoty);
	}

    private void initialize_particle()
    {
        //Randomize particle color 
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.color = ColorTable.GetRandom();

        //Randomize starting rotation
        int rand = Random.Range(0, 4);
        switch(rand)
        {
            case 0:
                transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                break;
            case 1:
                transform.rotation = Quaternion.Euler(90.0f, 0.0f, 0.0f);
                break;
            case 2:
                transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
                break;
            case 3:
                transform.rotation = Quaternion.Euler(0.0f, 0.0f, 90.0f);
                break;
        }        
    }

    private Vector3 InitializeLinearVelocity()
    {
        float x = Random.Range(0.0f, 1.0f);
        float z = Random.Range(0.0f, 0.5f);
        float y = Random.Range(1.0f, 3.0f);

        bool xNegate = Random.Range(1, 3) == 1;
        bool yNegate = Random.Range(1, 3) == 1;

        if (xNegate)
            x *= -1;
        if (yNegate)
            z *= -1;

        return new Vector3(x, -y, z);
    }

    private Vector3 InitializeAngularVelocity()
    {
        float xFactor = Random.Range(1, 3);
        float zFactor = Random.Range(1, 4);
        bool xNegate = Random.Range(1, 3) == 1;
        bool yNegate = Random.Range(1, 3) == 1;

        if (xNegate)
            xFactor *= -1;
        if (yNegate)
            zFactor *= -1;

        Debug.Log("speed: " + new Vector3(xFactor, 0.0f, zFactor));
        return new Vector3(xFactor, 0.0f, zFactor);
    }
}
