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
    public Text rankingMessage;
    protected EndgameController() { }
    public int numOfSecAfterEndPath = 5;

    PathFollower player;

    private float pointsOfThreshold = 0;
    private List<int> timeRanking = new List<int>();

    private void Awake() {
        
    }

    void Start () {
        player = GetComponent<PathFollower>();
        skyboxVideoPlayer.loopPointReached += EndReached;
	}

	void Update () {
        if (player.IsDone()) {
            GameOver();
        }
	}

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
       
    }

    public void GameOver() {
        endingMessage.text = "End of you journey. You collected " + coinCounter.GetComponent<CoinCounter>().count.ToString() + " coins";
        rankingMessage.text = "Ranking \n SONIA IS THE BEST";
        StartCoroutine(WaitFewSeconds());
        //Show the Ranking here
        //Add the average speed to the ranking here
    }

    private void resetNewPlayer() {
        //Maybe reset the videoplayer here
        endingMessage.text = "";
        pointsOfThreshold = 0.0f;
    }

    public void GiveUserPoints(int point) {
        pointsOfThreshold += point;
    }

    IEnumerator WaitFewSeconds() {
        yield return new WaitForSeconds(numOfSecAfterEndPath);
        skyboxVideoPlayer.Stop(); // We stop the video
        SceneManager.LoadScene("MainMenu");
        resetNewPlayer();
        print("We waited 5 seconds :D");
    }
}
