using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TimeController : MonoBehaviour {

    [SerializeField] private float rewardInterval;
    [SerializeField] private float rewardDuration;
    [SerializeField] private GameObject milestonePanel;
    [SerializeField] private GameObject timePanel;
    [SerializeField] private GameObject fireworkPrefab;
    [SerializeField] private GameObject confettiSystemPrefab;
    [SerializeField] private GameObject camera;

    [SerializeField] private AudioSource fireworkAudioSource;

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
        "Biking saves money",
        "Biking burns calories",
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

        fireworkAudioSource.clip = MakeSubclip(fireworkAudioSource.clip, 6.0f, 10.0f);
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

        float offsetX = confettiSystemPrefab.GetComponent<BoxCollider>().size.x;
        float offsetY = confettiSystemPrefab.GetComponent<BoxCollider>().size.y;
        float offsetZ = confettiSystemPrefab.GetComponent<BoxCollider>().size.z;
        Vector3 offset = new Vector3(offsetX / 4, offsetY * 3, -offsetZ / 2);
        GameObject confetti = Instantiate(confettiSystemPrefab, camera.transform.position + offset, Quaternion.identity);

        bool soundPlayed = false;
        float timeActive = 0.0f;
        milestonePanel.SetActive(true);
        while (timeActive < rewardDuration)
        {
            if(!soundPlayed)
            {
                float reject = Random.Range(0.0f, 10.0f);
                if(reject > 9.5)
                {
                    Debug.Log("Playing sounds at " + timeActive);
                    fireworkAudioSource.Play();
                    soundPlayed = true;
                }
            }

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

        for (int i = 0; i < 4; i++)
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

    /**
    * Creates a sub clip from an audio clip based off of the start time
    * and the stop time. The new clip will have the same frequency as
    * the original.
    */
    private AudioClip MakeSubclip(AudioClip clip, float start, float stop)
    {
        /* Create a new audio clip */
        int frequency = clip.frequency;
        float timeLength = stop - start;
        int samplesLength = (int)(frequency * timeLength);
        AudioClip newClip = AudioClip.Create(clip.name + "-sub", samplesLength, 1, frequency, false);
        /* Create a temporary buffer for the samples */
        float[] data = new float[samplesLength];
        /* Get the data from the original clip */
        clip.GetData(data, (int)(frequency * start));
        /* Transfer the data to the new clip */
        newClip.SetData(data, 0);
        /* Return the sub clip */
        return newClip;
    }
}
