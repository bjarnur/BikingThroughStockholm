using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class EndgameController : MonoBehaviour
{
    public VideoPlayer skyboxVideoPlayer;
    public CoinCounter coinCounter;
    public TextMeshProUGUI endingMessage;
    public int numOfSecAfterEndPath = 5;
    public PickupGatherer gatherer;

    PathFollower player;

    private float pointsOfThreshold = 0;
    private List<int> timeRanking = new List<int>();
   
    

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
        endingMessage.text = "End of your journey!\nYou collected " + coinCounter.GetComponent<CoinCounter>().count.ToString() + " coins";
        player.enabled = false;
        gatherer.enabled = false;
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
