using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeRecord : MonoBehaviour
{
    private List<float> timeRecord = new List<float>();

    public void WriteList(List<float> times)
    {
        timeRecord = times;
    }

    public List<float> ReturnList()
    {
        return timeRecord;
    }
}
