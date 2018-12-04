using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class ColorTable
{
    public enum HueColorNames
    {
        Lime = 0,
        Green = 1,
        Aqua = 2,
        Blue = 3,
        Navy = 4,
        Purple = 5,
        Pink = 6,
        Red = 7,
        Orange = 8,
        Yellow = 9
    }

    public static Color32 GetColor(HueColorNames colorName)
    {
        return colorList[(int)colorName];
    }

    public static Color32 GetRandom()
    {
        int idx = Random.Range(0, colorList.Count);
        return colorList[idx];
    }

    private static List<Color32> colorList = new List<Color32>
    {
        new Color32( 166 , 254 , 0, 1 ),
        new Color32( 0 , 254 , 111, 1 ),
        new Color32( 0 , 201 , 254, 1 ),
        new Color32( 0 , 122 , 254, 1 ),
        new Color32( 60 , 0 , 254, 1 ),
        new Color32( 143 , 0 , 254, 1 ),
        new Color32( 232 , 0 , 254, 1 ),
        new Color32( 254 , 9 , 0, 1 ),
        new Color32( 254 , 161 , 0, 1 ),
        new Color32( 254 , 224 , 0, 1 )
    };    
}
