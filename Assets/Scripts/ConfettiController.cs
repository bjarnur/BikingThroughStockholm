using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfettiController : MonoBehaviour {

    [SerializeField] private float gravity;

    private Vector3 linearVelocity;
    private Vector3 angularVelicoty;

	void Start ()
    {
        linearVelocity = InitializeLinearVelocity();
        angularVelicoty = InitializeAngularVelocity();
    }
	
	void Update ()
    {
        transform.position += linearVelocity * Time.deltaTime;
        transform.Rotate(angularVelicoty);
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
