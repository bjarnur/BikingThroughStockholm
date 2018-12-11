using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsPath : MonoBehaviour {
    public GameObject pickupPrefab;
    public float rowOffset = 1;

    List<Vector3> keyPoints;
    float totalDistance;

    private void Awake()
    {
        keyPoints = new List<Vector3>();
        totalDistance = 0;

        AddPoint(Vector3.zero);
        for (int i = 1; i < 10; i++)
        {
            AddPoint(10 * new Vector3(i, 0, Mathf.Cos(i)));
        }
        for (float i = 0.1f; i < 1; i += 0.1f)
        {
            AddPickupSerie(i, Random.Range(2, 5), 1.5f, Random.value < 0.5f ? PathPosition.LEFT : PathPosition.RIGHT);
        }
    }

    public int PointCount {
        get
        {
            return keyPoints.Count;
        }
    }

    public Vector3 this[int index]
    {
        get { return keyPoints[index]; }
    }

    public void AddPoint(Vector3 pos)
    {
        if (keyPoints.Count > 0)
        {
            totalDistance += Vector3.Distance(keyPoints[keyPoints.Count - 1], pos);
        }
        keyPoints.Add(pos);
    }

    public void AddPickupSerie(float startDistance, int amount, float absoluteStep, PathPosition pathPos)
    {
        for (int i = 0; i < amount; i++)
        {
            AddPickup(startDistance + i * (absoluteStep / totalDistance), pathPos);
        }
    }

    public void AddPickup(float relativeDistance, PathPosition pathPos)
    {
        GameObject pickup = Instantiate<GameObject>(pickupPrefab, transform);
        pickup.GetComponent<BelongToPath>().position = pathPos;
        pickup.transform.localPosition = DistanceToPosition(relativeDistance) + Vector3.up * 0.5f;
        Vector3 dir = DistanceToDirection(relativeDistance);
        if (pathPos == PathPosition.LEFT)
        {
            pickup.transform.localPosition += Quaternion.Euler(0, -90, 0) * dir * rowOffset;
        } else if (pathPos == PathPosition.RIGHT)
        {
            pickup.transform.localPosition += Quaternion.Euler(0, 90, 0) * dir * rowOffset;
        }
    }

    // The position an object moving along the path has when it is at "relativeDistance" amount of progress
    public Vector3 DistanceToPosition(float relativeDistance)
    {
        relativeDistance = Mathf.Clamp(relativeDistance, 0, 1);
        float goalDistance = relativeDistance * totalDistance;
        float counter = 0;
        int nextPoint = 1;
        float nextDistance = Vector3.Distance(this[nextPoint - 1], this[nextPoint]);

        while (counter + nextDistance < goalDistance)
        {
            counter += nextDistance;
            nextPoint++;
            nextDistance = Vector3.Distance(this[nextPoint - 1], this[nextPoint]);
        }

        Vector3 dir = (this[nextPoint] - this[nextPoint - 1]).normalized;
        return this[nextPoint - 1] + dir * (goalDistance - counter);
    }

    // The direction an object moving along the path should have when being at that distance
    public Vector3 DistanceToDirection(float relativeDistance)
    {
        relativeDistance = Mathf.Clamp(relativeDistance, 0, 1);
        float goalDistance = relativeDistance * totalDistance;
        float counter = 0;
        int nextPoint = 1;
        float nextDistance = Vector3.Distance(this[nextPoint - 1], this[nextPoint]);

        while (counter + nextDistance < goalDistance)
        {
            counter += nextDistance;
            nextPoint++;
            nextDistance = Vector3.Distance(this[nextPoint - 1], this[nextPoint]);
        }

        return (this[nextPoint] - this[nextPoint - 1]).normalized;
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        foreach (Vector3 point in keyPoints)
        {
            Gizmos.DrawWireSphere(point, 1f);
        }

        for (int i = 0; i < keyPoints.Count - 1; i++)
        {
            Gizmos.DrawLine(keyPoints[i], keyPoints[i + 1]);
        }
    }
}
