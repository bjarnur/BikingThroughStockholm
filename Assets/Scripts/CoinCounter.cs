using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCounter : MonoBehaviour {
    public PickupGatherer gatherer;
    public Text counterText;

    [HideInInspector]
    public int count = 0;
    int goal;

	void Start () {
        gatherer.OnPickup += OnPickup;
        count = 0;
        goal = GameObject.FindGameObjectsWithTag("Pickup").Length;
        counterText.text = count + " / " + goal;
    }

    void OnPickup(GameObject pickup)
    {
        count++;
        counterText.text = count + " / " + goal;
    }
	
	void Update () {
		
	}
}
