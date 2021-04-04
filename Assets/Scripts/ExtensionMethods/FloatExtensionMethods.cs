using System.Collections;
using UnityEngine;

//It is common to create a class to contain all of your
//extension methods. This class must be static.
public static class FloatExtensionMethods
{
    public static float Map(this float x, float in_min, float in_max, float out_min, float out_max) {
        x = (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
        return x;
    }
}