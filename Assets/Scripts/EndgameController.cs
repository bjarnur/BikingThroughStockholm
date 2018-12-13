using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndgameController : Singleton<EndgameController> {

    public VideoPlayer skyboxVideoPlayer;
    public GameObject cointCounter;
    public Text endingMessage;
    protected EndgameController() { }

    private float speedAverage = 0.0f;

    private List<int> timeRanking = new List<int>();

    private void Awake() {
        
    }

    // Use this for initialization
    void Start () {
        skyboxVideoPlayer.loopPointReached += EndReached;
	}
	
	// Update is called once per frame
	void Update () {
       
	}

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        GameOver();
    }

    public void GameOver() {
        skyboxVideoPlayer.Stop(); // We stop the video

        endingMessage.text = "End of you journey. You collected " + cointCounter.GetComponent<CoinCounter>().count.ToString() + " coins";

        System.Threading.Thread.Sleep(5000);
        SceneManager.LoadScene("MainMenu");

        //Show the Ranking here
        //Add the average speed to the ranking here
        resetNewPlayer();
    }

    private void resetNewPlayer() {
        //Maybe reset the videoplayer here
        speedAverage = 0.0f;
    }

    public void TrackUserSpeed(float speed) {
        Debug.Log("The user speed is: " + speed.ToString());
        // 
    }
}
