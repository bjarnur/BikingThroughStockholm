using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class EndgameController : MonoBehaviour {

    public VideoPlayer skyboxVideoPlayer;
    public CoinCounter coinCounter;
    public Text endingMessage;

    PathFollower player;

    private SortedList<int, string> rankings;

	// Use this for initialization
	void Start () {
        player = GetComponent<PathFollower>();
        skyboxVideoPlayer.loopPointReached += EndReached;
	}
	
	// Update is called once per frame
	void Update () {
        if (player.IsDone())
        {
            endingMessage.text = "End of you journey.\nYou collected " + coinCounter.count.ToString() + " coins";
            endingMessage.gameObject.SetActive(true);
        } else
        {
            endingMessage.gameObject.SetActive(false);
        }
	}

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        player.Reset();
        coinCounter.Reset();
    }
}
