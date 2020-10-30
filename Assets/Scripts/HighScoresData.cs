using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HighScoresData
{
    public double[] easy, medium, hard;
    public HighScoresData(double[] c_easy, double[] c_medium, double[] c_hard)
    {
        easy = c_easy;
        medium = c_medium;
        hard = c_hard;
    }
}
