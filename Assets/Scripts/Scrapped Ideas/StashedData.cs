using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StashedData", menuName = "ScriptableObjects/DataStasher", order = 1)]
public class StashedData : ScriptableObject
{
    public List<float> recordedTime = new List<float>();
    public List<int> recordedAudio = new List<int>();

    public void WriteData(List<float> times, List<int> audioNums)
    {
        recordedTime = times;
        recordedAudio = audioNums;
    }

    public void ClearData()
    {
        recordedTime = new List<float>();
        recordedAudio = new List<int>();
    }
}
