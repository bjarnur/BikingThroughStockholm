using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class RhythmTracker : MonoBehaviour
{

    public Image ProgressBar;
    public Image fadePanel;
    public List<GameObject> targetBars;
    public int levelDuration = 40;

    private int levelIdx;
    private float levelGoal;
    private float progress;
    private float loseTime = 5.0f;
    private float timeLeft;
    private float switchTimer;

    private bool simulationStarted = false;
    private float alpha = 0.0f;

	// Use this for initialization
	void Start ()
    {
        timeLeft = loseTime;
        progress = 0.8f;

        //Level 1, naturally
        levelIdx = 2;
        levelGoal = 0.2f;
        switchTimer = 0.0f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //videoRenderer.material.color = new Color(videoRenderer.material.color.r, videoRenderer.material.color.g, videoRenderer.material.color.b, 0.5f);
        //progress = Mathf.Max(progress - 0.1f * Time.deltaTime, 0f);
        if (Input.GetKeyDown("space"))
        {
            if (!simulationStarted)
                StartSimulation();

            progress = Mathf.Min(progress + 0.1f, 1.0f);
        }

        if (simulationStarted)
        {
            ProgressBar.fillAmount = progress;
            if (progress < levelGoal) {
                timeLeft -= Time.deltaTime;
                ProgressBar.color = Color.red;
            } else {
                timeLeft = loseTime;
                ProgressBar.color = Color.green;
            }

            alpha = (loseTime - Mathf.Max(timeLeft, 0.0f)) / loseTime;

            if (timeLeft <= 0) {
                GameOver();
            }
        }

        fadePanel.color = new Color(fadePanel.color.r, fadePanel.color.g, fadePanel.color.b, alpha);

        switchTimer += Time.deltaTime;
        if (switchTimer > levelDuration)
            SwitchLevel();
    }

    private void GameOver() {
        EndgameController.Instance.GameOver();
    }

    public void UpdateRhythm()
    {
        if (!simulationStarted)
            StartSimulation();

        progress = Mathf.Min(progress + 0.1f, 1.0f);
        ProgressBar.fillAmount = progress;
    }

    public void UpdateRhythm(float speed)
    {
        if (!simulationStarted && speed > 0)
        {
            Debug.Log("Starting simulation. Speed: " + speed);
            StartSimulation();
        }            

        //progress = Mathf.Min(progress + 0.1f, 1.0f);
        progress = Mathf.Min(speed, 1.0f);
        ProgressBar.fillAmount = progress;

    }

    private void SwitchLevel()
    {
        targetBars[levelIdx].SetActive(false);
        levelIdx = Random.Range(0, 3);
        targetBars[levelIdx].SetActive(true);

        switch(levelIdx)
        {
            case 0:
                levelGoal = 0.7f;
                break;
            case 1:
                levelGoal = 0.5f;
                break;
            case 2:
                levelGoal = 0.2f;
                break;
        }

        switchTimer = 0.0f;
    }

    private void StartSimulation()
    {        
        GameObject.FindWithTag("BikeFootage").GetComponent<VideoPlayer>().Play();
        GameObject.FindWithTag("PlayerContainer").GetComponent<PathFollower>().enabled = true;
        simulationStarted = true;
    }
}
