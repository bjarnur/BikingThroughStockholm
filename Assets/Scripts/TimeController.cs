using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour {

    [SerializeField] private float rewardInterval;
    [SerializeField] private float rewardDuration;
    [SerializeField] GameObject milestonePanel;
    [SerializeField] GameObject timePanel;
    [SerializeField] GameObject fireworkPrefab;

    private GameObject milestonePrompt;
    private GameObject complimentPrompt;
    private GameObject timePrompt;

    private float timeSoFar;
    private float lastMilestone;
    private string secondsSoFar;
    private string minutesSoFar;

    private Text timeText;
    private Text milestoneText;
    private Text complimentText;

    private static List<string> compliments = new List<string>
    {
        "Go you!!", 
        "Well done!!",
        "WOW!",
        "Biking saves money and burns calories",
        "Keep up the good work!!",
        "Good job :)",
        "Nicely done!"
    };

	void Start ()
    {
        timeSoFar = 0.0f;

        milestonePrompt = milestonePanel.transform.GetChild(0).gameObject;
        complimentPrompt = milestonePanel.transform.GetChild(1).gameObject;
        timePrompt = timePanel.transform.GetChild(0).gameObject;

        timeText = timePrompt.GetComponent<Text>();
        milestoneText = milestonePrompt.GetComponent<Text>();
        complimentText = complimentPrompt.GetComponent<Text>();
	}
	
	
	void Update ()
    {
        UpdateTimer();
        CheckMilestones();
    }

    private void UpdateTimer()
    {
        timeSoFar += Time.deltaTime;
        minutesSoFar = Mathf.Floor(timeSoFar / 60).ToString("00");
        secondsSoFar = (timeSoFar % 60).ToString("00");
        timeText.text = minutesSoFar + ":" + secondsSoFar;
    }

    private void CheckMilestones()
    {
        float progressSinceLastMilestone = timeSoFar - lastMilestone;
        if(progressSinceLastMilestone > rewardInterval)
        {
            Debug.Log("progress since last " + progressSinceLastMilestone);
            Debug.Log("time so far " + timeSoFar);
            Debug.Log("reward intervarl " + rewardInterval);
            StartCoroutine("DisplayReward");
            lastMilestone = timeSoFar;
        }
    }

    private IEnumerator DisplayReward()
    {        
        PrepareMilestonePanel();
        List<GameObject> fireworks = LaunchFireworks();

        float timeActive = 0.0f;
        milestonePanel.SetActive(true);
        while (timeActive < rewardDuration)
        {
            timeActive += Time.deltaTime;
            yield return null;
        }

        milestonePanel.SetActive(false);
        DestroyFireworks(fireworks);
        yield break;
    }

    private void PrepareMilestonePanel()
    {
        int complimentIndex = Random.Range(0, compliments.Count);
        milestoneText.text = minutesSoFar + ":" + secondsSoFar;
        complimentText.text = compliments[complimentIndex];
    }

    private List<GameObject> LaunchFireworks()
    {
        List<GameObject> res = new List<GameObject>();
        RectTransform rect = milestonePanel.GetComponent<RectTransform>();

        for(int i = 0; i < 4; i++)
        {
            GameObject fw = Instantiate(fireworkPrefab, milestonePanel.transform, false);
            res.Add(fw);

            var particleSys = fw.GetComponent<ParticleSystem>().main;
            particleSys.startColor = Random.ColorHSV();

            switch(i)
            { 
                case 0:
                    fw.transform.localPosition += new Vector3(rect.offsetMin.x, rect.offsetMax.y / 2, 0);
                    break;
                case 1:
                    fw.transform.localPosition -= new Vector3(rect.offsetMin.x, rect.offsetMax.y / 2, 0);
                    break;
                case 2:
                    fw.transform.localPosition -= new Vector3(rect.offsetMax.x, rect.offsetMax.y / 2, 0);
                    break;
                case 3:
                    fw.transform.localPosition += new Vector3(rect.offsetMax.x, rect.offsetMax.y / 2, 0);
                    break;
            }
        }
        return res;
    }

    private void DestroyFireworks(List<GameObject> fireworks)
    {
        foreach(GameObject firework in fireworks)
        {
            GameObject.Destroy(firework);
        }
    }
}
