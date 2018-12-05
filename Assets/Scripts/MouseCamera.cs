using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCamera : MonoBehaviour {
    bool mousePressed = false;
    Vector3 startPosition;

    Vector3 screenCenterer;
    Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        screenCenterer = new Vector3(Screen.width / 2, Screen.height / 2);
    }

    void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            startPosition = Input.mousePosition - screenCenterer;
            mousePressed = true;
        }
        if (Input.GetMouseButtonUp(0)) mousePressed = false;

        if (mousePressed)
        {
            Vector3 moveDelta = (Input.mousePosition - screenCenterer - startPosition) * 80;
            cam.transform.localRotation = Quaternion.Euler(Vector3.up * moveDelta.x / Screen.width - Vector3.right * moveDelta.y / Screen.height);
        }
	}
}
