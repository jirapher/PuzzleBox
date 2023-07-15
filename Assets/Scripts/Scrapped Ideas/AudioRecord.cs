using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioRecord : MonoBehaviour
{
    private List<AudioSource> audioRecord = new List<AudioSource>();

    public void WriteList(List<AudioSource> audioList)
    {
        audioRecord = audioList;
    }

    public List<AudioSource> ReturnList()
    {
        return audioRecord;
    }
}
