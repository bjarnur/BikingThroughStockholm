using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndgameController : Singleton<EndgameController> {

    public VideoPlayer skyboxVideoPlayer;
    public CoinCounter coinCounter;
    public Text endingMessage;
    protected EndgameController() { }

    PathFollower player;

    private float speedAverage = 0.0f;
    private List<int> timeRanking = new List<int>();
    private void Awake() {
        
    }

    void Start () {
        player = GetComponent<PathFollower>();
        skyboxVideoPlayer.loopPointReached += EndReached;
	}

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
        //player.Reset();
        //coinCounter.Reset();
        GameOver();
    }

    public void GameOver() {
        skyboxVideoPlayer.Stop(); // We stop the video

        endingMessage.text = "End of you journey. You collected " + coinCounter.GetComponent<CoinCounter>().count.ToString() + " coins";

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
