using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class GPSConverter : MonoBehaviour {

    private List<Vector2> pathPointsGPS = new List<Vector2>();
    private List<Vector3> pathPoints = new List<Vector3>();
    
    void Start () {
        string GPSFile = "Assets/GPSTracks/track_YDXJ0035.xml";
        FromGPSToXYZ(GPSFile, pathPointsGPS, pathPoints);
        //pathpoints: Final list with the coordinates of all points in the path converted to XYZ 
                      //and contained in "floor" plane

    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        foreach (Vector3 point in pathPoints)
        {
            Gizmos.DrawWireSphere(point, 1f);
        }

        for (int i = 0; i < pathPoints.Count - 1; i++)
        {
            Gizmos.DrawLine(pathPoints[i], pathPoints[i + 1]);
        }
        Gizmos.DrawLine(Vector3.zero, pathPoints[0]);

    }

    void FromGPSToXYZ(string GPSFile, List<Vector2> pathPointsGPS, List<Vector3> pathPoints)
    {
        System.Globalization.NumberStyles style;
        System.Globalization.CultureInfo culture;
        Vector3 offset = Vector3.zero;
        //string latitude = "";
        //string longitude = "";

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(GPSFile);

        foreach (XmlNode xmlNode in xmlDoc.DocumentElement.ChildNodes[1].ChildNodes[3])
        {
            //latitude = latitude + xmlNode.Attributes["lat"].Value + "; ";
            //longitude = longitude + xmlNode.Attributes["lon"].Value + "; ";

            style = System.Globalization.NumberStyles.AllowDecimalPoint;
            culture = System.Globalization.CultureInfo.CreateSpecificCulture("en-GB");
            float lat, lon;
            if (float.TryParse(xmlNode.Attributes["lat"].Value, style, culture, out lat) &&
                float.TryParse(xmlNode.Attributes["lon"].Value, style, culture, out lon))
            {
                pathPointsGPS.Add(new Vector2 ( lat, lon ));

                float r = 6371e3f; // metres

                lat = 90f - lat;
                float x = r * Mathf.Sin(lat * Mathf.Deg2Rad) * Mathf.Cos(lon * Mathf.Deg2Rad);
                float y = r * Mathf.Sin(lat * Mathf.Deg2Rad) * Mathf.Sin(lon * Mathf.Deg2Rad);
                float z = r * Mathf.Cos(lat * Mathf.Deg2Rad);

                if (pathPoints.Count == 0)
                {
                    //Vector from the Earth's center to the first point in path
                    offset = new Vector3(x, y, z) - Vector3.zero;
                }
                pathPoints.Add(new Vector3(x, y, z) - offset);

            }
            else
                print("Unable to convert coordinates");
        }

        //Easiest way I found to rotate all the path so that it lays in the "floor" plane
        foreach (Vector3 point in pathPoints)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = point;
            cube.transform.parent = this.transform;
            cube.SetActive(false);
        }
        // Sets the rotation so that the transform's offset-axis goes along the y-axis
        transform.rotation = Quaternion.FromToRotation(offset, -Vector3.up);
        for (int i = 0; i < pathPoints.Count; i++)
        {
            //Final list with the coordinates of all points in the path
            pathPoints[i] = this.gameObject.transform.GetChild(i).position;
        }
    }

}
