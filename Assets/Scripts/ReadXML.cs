using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class ReadXML : MonoBehaviour {

    private List<float[]> pathPointsGPS = new List<float[]>();
    private List<Vector3> pathPoints = new List<Vector3>();

    System.Globalization.NumberStyles style;
    System.Globalization.CultureInfo culture;

    Vector3 offset;
    float latAngle;

    string latitude = "";
    string longitude = "";

    void Start () {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load("Assets/GPSTracks/track_YDXJ0035.xml");

        foreach (XmlNode xmlNode in xmlDoc.DocumentElement.ChildNodes[1].ChildNodes[3])
        {
            latitude = latitude + xmlNode.Attributes["lat"].Value + "; ";
            longitude = longitude + xmlNode.Attributes["lon"].Value + "; ";

            //value = "1345,978";
            style = System.Globalization.NumberStyles.AllowDecimalPoint;
            culture = System.Globalization.CultureInfo.CreateSpecificCulture("en-GB");
            float lat, lon;
            if (float.TryParse(xmlNode.Attributes["lat"].Value, style, culture, out lat) &&
                float.TryParse(xmlNode.Attributes["lon"].Value, style, culture, out lon))
            {
                pathPointsGPS.Add(new float[2] { lat, lon });

                float r = 6371e3f; // metres

                lat = 90f - lat;
                //latAngle = lat;
                float x = r * Mathf.Sin(lat * Mathf.Deg2Rad) * Mathf.Cos(lon * Mathf.Deg2Rad);
                float y = r * Mathf.Sin(lat * Mathf.Deg2Rad) * Mathf.Sin(lon * Mathf.Deg2Rad);
                float z = r * Mathf.Cos(lat * Mathf.Deg2Rad);

                if (pathPoints.Count == 0)
                {
                    offset = new Vector3(x, y, z) - Vector3.zero;
                }

                //pathPoints.Add(new Vector3(x, y, z));
                pathPoints.Add(new Vector3(x, y, z) - offset);

            }
            else
                print("Unable to convert coordinates");
        }
        print("latitude: " + latitude);
        print("long: " + longitude);

        //Vector3 offset = pathPoints[0] - Vector3.zero;

        //Vector3 newOffset = Vector3.zero;
        //bool first = true;
        foreach(Vector3 point in pathPoints)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = point;
            //cube.transform.RotateAround(Vector3.zero, Vector3.Cross(Vector3.up, offset), latAngle);
            
            //newOffset = first? cube.transform.position : newOffset;
            //first = false;
            //cube.transform.position = cube.transform.position - newOffset;
            //Vector3 offset = pathPoints[0] - Vector3.zero;
        }

    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        foreach (Vector3 point in pathPoints)
        {
            //Gizmos.DrawWireSphere(point, 1f);
        }

        for (int i = 0; i < pathPoints.Count - 1; i++)
        {
            Gizmos.DrawLine(pathPoints[i], pathPoints[i + 1]);
        }
        Gizmos.DrawLine(Vector3.zero, pathPoints[0]);

    }

}
