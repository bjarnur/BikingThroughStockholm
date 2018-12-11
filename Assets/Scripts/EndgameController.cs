using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class EndgameController : MonoBehaviour {

    public VideoPlayer skyboxVideoPlayer;
    public GameObject cointCounter;
    public Text endingMessage;

    private SortedList<int, string> rankings;

	// Use this for initialization
	void Start () {
        skyboxVideoPlayer.loopPointReached += EndReached;
	}
	
	// Update is called once per frame
	void Update () {
       
	}

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        endingMessage.text = "End of you journey. You collected " + cointCounter.GetComponent<CoinCounter>().count.ToString() + " coins";
        vp.Stop();
    }
}
