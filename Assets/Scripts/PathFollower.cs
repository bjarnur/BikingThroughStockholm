using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour {
    public PointsPath path;
    public int duration;
    public float convergingSpeed = 0.2f;

    float speed;
    bool done;
    Vector3 wantedDir;
    Vector3 lastDir;
    int nextPoint;

    public void Reset()
    {
        done = false;
        nextPoint = 0;
        lastDir = (path[1] - path[0]).normalized;
        speed = path.GetTotalDistance() / duration;
        transform.localPosition = path[0];
        path.RespawnPickups();
        GoToNextPoint();
    }

    public bool IsDone()
    {
        return done;
    }

    void Start()
    {
        Reset();
    }

    void Update()
    {
        if (!done)
        {
            lastDir = lastDir * (1 - convergingSpeed) + wantedDir * convergingSpeed;
            transform.localPosition += lastDir * speed * Time.deltaTime;
            //transform.LookAt(transform.position + lastDir);
            if (Vector3.Distance(transform.localPosition, path[nextPoint]) < 1f)
            {
                GoToNextPoint();
            }
        }
    }

    void GoToNextPoint()
    {
        nextPoint++;
        if (nextPoint >= path.PointCount)
        {
            done = true;
            return;
        }
        wantedDir = (path[nextPoint] - path[nextPoint - 1]).normalized;
    }
}
