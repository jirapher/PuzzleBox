using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputRecorder : MonoBehaviour
{
    public static InputRecorder instance;

    [Header("Recording")]
    public float timer = 0;
    public List<float> recordedTime = new List<float>();
    public List<AudioSource> recordedAudio = new List<AudioSource>();
    public bool recording = false;


    [Header("Stashing")]
    public StashedData dataValuesSO;
    public List<StashedData> allData;
    public int curDataCount;

    private void Start()
    {
        instance = this;
        ClearAllStashed();
    }

    private void Update()
    {
        if (recording)
        {
            timer += Time.deltaTime;
        }
    }

    private void ClearAllStashed()
    {
        for(int i = 0; i < allData.Count; i++)
        {
            allData[i].ClearData();
        }
    }
    public void LogNewNote(AudioSource clip)
    {
        recordedAudio.Add(clip);
        recordedTime.Add(timer);
        timer = 0;
    }

    public IEnumerator Playback()
    {
        int tick = 0;
        AudioSource curAudio = null;
        float curTime = 0;


        while(tick < recordedAudio.Count - 1)
        {
            curAudio = recordedAudio[tick];
            curTime = recordedTime[tick];

            while(curTime > 0)
            {
                curTime -= Time.deltaTime;
                yield return null;
            }

            //send input over
            AudioManager.instance.PlaySound(curAudio);
            tick++;
            yield return null;
        }

    }

    private IEnumerator PlaybackAll(List<float> times, List<int> audioNum)
    {
        int tick = 0;
        int curAudio = 0;
        float curTime = 0;

        while (tick < times.Count - 1)
        {
            curAudio = audioNum[tick];
            curTime = times[tick];

            while (curTime > 0)
            {
                curTime -= Time.deltaTime;
                yield return null;
            }

            //AudioManager.instance.PlaySound(curAudio);

            AudioManager.instance.PlayIndexSound(curAudio);

            tick++;
            yield return null;
        }
    }

    public void StartRecording()
    {
        timer = 0;
        recording = true;
    }

    public void StopRecording()
    {
        //timer = 0;
        recording = false;
    }

    public void ClearRecording()
    {
        recording = false;
        recordedAudio.Clear();
        recordedTime.Clear();
        timer = 0;
    }

    public void StashRecording()
    {
        List<int> temp = new List<int>();
        List<float> time = new List<float>();

        foreach(AudioSource a in recordedAudio)
        {
            temp.Add(AudioManager.instance.GetAudioNumber(a));
        }

        foreach(float t in recordedTime)
        {
            time.Add(t);
        }

        allData[curDataCount].WriteData(time, temp);

        curDataCount++;
        //stashedTimes[placeToStash] = recordedTime;
        //stashedAudio[placeToStash] = recordedAudio;
        //GameObject t = Instantiate(timeRecordPrefab);
        //GameObject a = Instantiate(audioRecordPrefab);

        //t.GetComponent<TimeRecord>().WriteList(recordedTime);
        //a.GetComponent<AudioRecord>().WriteList(recordedAudio);

        //stashedTimes.Add(t.GetComponent<TimeRecord>());
        //stashedAudio.Add(a.GetComponent<AudioRecord>());

        ClearRecording();
    }

    public void PlayAllRecordings()
    {
        foreach(StashedData s in allData)
        {
            if(s.recordedAudio.Count > 0)
            {
                StartCoroutine(PlaybackAll(s.recordedTime, s.recordedAudio));
            }
            
        }
    }
}
