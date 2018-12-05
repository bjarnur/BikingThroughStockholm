using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class ReadXML : MonoBehaviour {

    private List<float[]> pathPoints = new List<float[]>();
    System.Globalization.NumberStyles style;
    System.Globalization.CultureInfo culture;


    void Start () {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load("Assets/GPSTracks/track_YDXJ0035.xml");

        foreach (XmlNode xmlNode in xmlDoc.DocumentElement.ChildNodes[1].ChildNodes[3])
        {
            Debug.Log("Lat: " + xmlNode.Attributes["lat"].Value + ", Lon: " + xmlNode.Attributes["lon"].Value);

            //value = "1345,978";
            style = System.Globalization.NumberStyles.AllowDecimalPoint;
            culture = System.Globalization.CultureInfo.CreateSpecificCulture("en-GB");
            float lat, lon;
            if (float.TryParse(xmlNode.Attributes["lat"].Value, style, culture, out lat) &&
                float.TryParse(xmlNode.Attributes["lon"].Value, style, culture, out lon))
            {
                //print("Converted: " + lat);
                pathPoints.Add(new float[2] { lat, lon });
            }
            else
                print("Unable to convert coordinates");
        }
    }

}
