using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Kinect = Windows.Kinect;

using Joint = Windows.Kinect.Joint;

public class BodySourceView : MonoBehaviour 
{
    public GameObject canvas;
    public Material BoneMaterial;
    public GameObject BodySourceManager;
    public float footHigh;
    public float footLow;
    public float detectionThreshold;
    public bool doSetup = true;

    private float cyclingSpeed = 0.0f;
    private bool isLeftFootUp = false;
    private float timeSpent = 0.0f;
    private float previousTimeSpent = 0.0f;
    private float speedFactor = 2.5f;
    private RhythmTracker rhythmTracker;

    private bool setupDone = false;
    private float maxHeight = -100f;
    private float minHeight = 100f;
    private int count = 0;

    private Dictionary<ulong, GameObject> _Bodies = new Dictionary<ulong, GameObject>();
    private List<Kinect.JointType> _joints = new List<Kinect.JointType>
    {
        Kinect.JointType.FootLeft,
        Kinect.JointType.FootRight,
    };
    private BodySourceManager _BodyManager;

    private Dictionary<Kinect.JointType, Kinect.JointType> _BoneMap = new Dictionary<Kinect.JointType, Kinect.JointType>()
    {
        { Kinect.JointType.FootLeft, Kinect.JointType.AnkleLeft },
        { Kinect.JointType.AnkleLeft, Kinect.JointType.KneeLeft },
        { Kinect.JointType.KneeLeft, Kinect.JointType.HipLeft },
        { Kinect.JointType.HipLeft, Kinect.JointType.SpineBase },
        
        { Kinect.JointType.FootRight, Kinect.JointType.AnkleRight },
        { Kinect.JointType.AnkleRight, Kinect.JointType.KneeRight },
        { Kinect.JointType.KneeRight, Kinect.JointType.HipRight },
        { Kinect.JointType.HipRight, Kinect.JointType.SpineBase },
        
        { Kinect.JointType.HandTipLeft, Kinect.JointType.HandLeft },
        { Kinect.JointType.ThumbLeft, Kinect.JointType.HandLeft },
        { Kinect.JointType.HandLeft, Kinect.JointType.WristLeft },
        { Kinect.JointType.WristLeft, Kinect.JointType.ElbowLeft },
        { Kinect.JointType.ElbowLeft, Kinect.JointType.ShoulderLeft },
        { Kinect.JointType.ShoulderLeft, Kinect.JointType.SpineShoulder },
        
        { Kinect.JointType.HandTipRight, Kinect.JointType.HandRight },
        { Kinect.JointType.ThumbRight, Kinect.JointType.HandRight },
        { Kinect.JointType.HandRight, Kinect.JointType.WristRight },
        { Kinect.JointType.WristRight, Kinect.JointType.ElbowRight },
        { Kinect.JointType.ElbowRight, Kinect.JointType.ShoulderRight },
        { Kinect.JointType.ShoulderRight, Kinect.JointType.SpineShoulder },
        
        { Kinect.JointType.SpineBase, Kinect.JointType.SpineMid },
        { Kinect.JointType.SpineMid, Kinect.JointType.SpineShoulder },
        { Kinect.JointType.SpineShoulder, Kinect.JointType.Neck },
        { Kinect.JointType.Neck, Kinect.JointType.Head },
    };

    void Start()
    {
        rhythmTracker = canvas.GetComponent<RhythmTracker>();
    }

    void Update () 
    {
        if (BodySourceManager == null)
        {
            return;
        }
        
        _BodyManager = BodySourceManager.GetComponent<BodySourceManager>();
        if (_BodyManager == null)
        {
            return;
        }
        
        Kinect.Body[] data = _BodyManager.GetData();
        if (data == null)
        {
            return;
        }
        
        List<ulong> trackedIds = new List<ulong>();
        foreach(var body in data)
        {
            if (body == null)
            {
                continue;
              }
                
            if(body.IsTracked)
            {
                trackedIds.Add (body.TrackingId);
            }
        }
        
        List<ulong> knownIds = new List<ulong>(_Bodies.Keys);
        
        // First delete untracked bodies
        foreach(ulong trackingId in knownIds)
        {
            if(!trackedIds.Contains(trackingId))
            {
                Destroy(_Bodies[trackingId]);
                _Bodies.Remove(trackingId);
            }
        }

        foreach(var body in data)
        {
            if (body == null)
            {
                continue;
            }
            
            if(body.IsTracked)
            {
                if(!_Bodies.ContainsKey(body.TrackingId))
                {
                    _Bodies[body.TrackingId] = CreateBodyObject(body.TrackingId);
                }
                timeSpent += Time.deltaTime;  // Maybe it's not the best place to set this, 2 bodies can cause to double count. Maybe for now it just works
                RefreshBodyObject(body, _Bodies[body.TrackingId]);
            }
        }
    }
    
    private GameObject CreateBodyObject(ulong id)
    {
        GameObject body = new GameObject("Body:" + id);
        //body.SetActive(false);
        
        // Create joints
        //foreach (Kinect.JointType joint in _joints)
        //{
        //    GameObject newJoint = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //    newJoint.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        //    newJoint.name = joint.ToString();

        //    newJoint.transform.parent = body.transform;
        //}
        for (Kinect.JointType jt = Kinect.JointType.SpineBase; jt <= Kinect.JointType.ThumbRight; jt++)
        {
            GameObject jointObj;
            if (jt == Kinect.JointType.FootLeft || jt == Kinect.JointType.FootRight)
            {
                jointObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                jointObj.transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else
            {
                jointObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                jointObj.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            }

            LineRenderer lr = jointObj.AddComponent<LineRenderer>();
            lr.SetVertexCount(2);
            lr.material = BoneMaterial;
            lr.SetWidth(0.05f, 0.05f);

            jointObj.name = jt.ToString();
            jointObj.transform.parent = body.transform;
        }

        return body;
    }
    
    private void RefreshBodyObject(Kinect.Body body, GameObject bodyObject)
    {
        for (Kinect.JointType jt = Kinect.JointType.SpineBase; jt <= Kinect.JointType.ThumbRight; jt++)
        {
            Kinect.Joint sourceJoint = body.Joints[jt];
            Kinect.Joint? targetJoint = null;
            
            if(_BoneMap.ContainsKey(jt))
            {
                targetJoint = body.Joints[_BoneMap[jt]];
            }
            
            Transform jointObj = bodyObject.transform.Find(jt.ToString());
            jointObj.localPosition = GetVector3FromJoint(sourceJoint);
            //Debug.Log("body outside " + body.TrackingId);
            if (jt == Kinect.JointType.FootLeft && jointObj.localPosition.x >= detectionThreshold)
            {
                //Debug.Log("body inside " + body.TrackingId);
                if (!setupDone && doSetup)
                {
                    SetupPedal(jointObj.localPosition);
                }
                manageCyclingTimes(jointObj.localPosition);
                //-4.2 (Down) -2.1 (Up) -> set the threshold at 3
            }
            
            LineRenderer lr = jointObj.GetComponent<LineRenderer>();
            if(targetJoint.HasValue)
            {
                lr.SetPosition(0, jointObj.localPosition);
                lr.SetPosition(1, GetVector3FromJoint(targetJoint.Value));
                lr.SetColors(GetColorForState (sourceJoint.TrackingState), GetColorForState(targetJoint.Value.TrackingState));
            }
            else
            {
                lr.enabled = false;
            }
        }
    }
    /****** Function to calculate the speed ********/
    private void manageCyclingTimes(Vector3 cyclingLocalPosition) {
        //Debug.Log("foot height " + cyclingLocalPosition.y + "foot X " + cyclingLocalPosition.x + "foot Z " + cyclingLocalPosition.z);
        if (cyclingLocalPosition.y < footLow && isLeftFootUp) {
            isLeftFootUp = false;
            calculateCyclingSpeed(); // We call here the speed func. This will use the previousTimeSpent and the current value of timeSpent
            previousTimeSpent = timeSpent; // THen we change the previousTimeSpent value for the next iteration
            timeSpent = 0.0f;
            Debug.Log("DOWN");
            //rhythmTracker.UpdateRhythm();
        }
        else if (cyclingLocalPosition.y > footHigh && !isLeftFootUp) {
            isLeftFootUp = true;
            calculateCyclingSpeed();
            previousTimeSpent = timeSpent;
            timeSpent = 0.0f;
            Debug.Log("UP");
        }

    }
    private void calculateCyclingSpeed() {
        cyclingSpeed = Mathf.Abs(timeSpent - previousTimeSpent) * speedFactor; // We get the difference in absolute val and we multiply it for some factor. This will have to be tuned.
        //print(cyclingSpeed);
    }

    public float getCyclingSpeed() {
        return cyclingSpeed;
    }

    public void SetupPedal(Vector3 footPos)
    {
        maxHeight = footPos.y > maxHeight ? footPos.y : maxHeight;
        minHeight = footPos.y < minHeight ? footPos.y : minHeight;
        count++;
        if (count == 500)
        {
            print("Max: " + maxHeight);
            print("Min: " + minHeight);

            footHigh = maxHeight - 1f; 
            footLow = minHeight + 1f;

            setupDone = true;
        }
    }
    
    /**********************************************/

    private static Color GetColorForState(Kinect.TrackingState state)
    {
        switch (state)
        {
        case Kinect.TrackingState.Tracked:
            return Color.green;

        case Kinect.TrackingState.Inferred:
            return Color.red;

        default:
            return Color.black;
        }
    }
    
    private static Vector3 GetVector3FromJoint(Kinect.Joint joint)
    {
        return new Vector3(joint.Position.X * 10, joint.Position.Y * 10, joint.Position.Z * 10);
    }


}
