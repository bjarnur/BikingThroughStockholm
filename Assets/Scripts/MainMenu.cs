using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public float delay;
    public GameObject gameCamera;

    float timeCounter;
    

    void Update()
    {
        RaycastHit hit;
        Debug.DrawLine(gameCamera.transform.position, gameCamera.transform.forward*5000, Color.green);
        if (Physics.Raycast(gameCamera.transform.position, gameCamera.transform.forward, out hit,5000))
        {
            GameObject objectHit = hit.transform.gameObject;
            if (objectHit.CompareTag("Button")) //Same counter is for all buttons right now...
            {
                objectHit.GetComponent<Button>().Select();
                timeCounter += Time.deltaTime;
                if(timeCounter >= delay)
                {
                    objectHit.GetComponent<Button>().onClick.Invoke();
                }

            }
            else
            {
                timeCounter = 0;
            }
        }
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("PathScene");
    }

}
