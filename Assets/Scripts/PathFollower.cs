using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour {
    public PointsPath path;
    public float speed = 1f;

    bool done;
    Vector3 direction;
    int nextPoint;

    public void Reset()
    {
        done = false;
        nextPoint = 0;
        GoToNextPoint();
    }

    void Start () {
        Reset();
    }

    void Update()
    {
        if (!done)
        {
            transform.localPosition += direction * speed / 10f;
            if (Vector3.Distance(transform.localPosition, path[nextPoint]) < 0.1f)
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
        direction = (path[nextPoint] - path[nextPoint - 1]).normalized;
        transform.LookAt(path[nextPoint]);
    }
}
