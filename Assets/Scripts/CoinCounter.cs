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
        Reset();
    }

    public void Reset()
    {
        count = 0;
        goal = GameObject.FindGameObjectsWithTag("Pickup").Length;
    }

    void OnPickup(GameObject pickup)
    {
        count++;
    }
	
	void Update () {
        counterText.text = count + " / " + goal;
    }
}
