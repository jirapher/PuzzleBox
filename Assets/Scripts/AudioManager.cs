using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private AudioSource curSound;
    private AudioSource[] curInstrument;

    public AudioSource[] allInstruments;

    private int curInstrumentNum = 0;
    private void Start()
    {
        instance = this;
    }

    public void PlaySound(AudioSource sound)
    {
        sound.PlayOneShot(sound.clip);
    }

    public void PlayIndexSound(int clipNum)
    {
        allInstruments[clipNum].Play();
    }

    public int GetAudioNumber(AudioSource clip)
    {
        //gets us the order number in all array
        for(int i = 0; i < allInstruments.Length; i++)
        {
            if(clip == allInstruments[i])
            {
                curInstrumentNum = i;
            }
        }

        return curInstrumentNum;
    }

    public void PitchUp()
    {
        curSound.pitch += 0.1f;
    }

    public void PitchDown()
    {
        curSound.pitch -= 0.1f;
    }

    public void PitchNormalizeCurInstrument()
    {
        foreach(AudioSource a in curInstrument)
        {
            a.pitch = 1;
        }
    }
}
