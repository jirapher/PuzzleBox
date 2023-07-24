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
    public float bkgFadePoint = .2f;
    public AudioSource[] sfx;
    public AudioSource[] bkg;
    public float fadeDuration = 2f;

    private void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        
        PlayBKG(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PlayBKG(1);
        }
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

    public void PlaySFX(int sfxNum)
    {
        sfx[sfxNum].PlayOneShot(sfx[sfxNum].clip);
    }

    public void PlayBKG(int bkgNum)
    {
        StopBKG();
        bkg[bkgNum].volume = 0;
        bkg[bkgNum].Play();
        StartCoroutine(FadeAudioSource(bkg[bkgNum], 1));
    }

    public void LowerBKG()
    {
        for (int i = 0; i < bkg.Length; i++)
        {
            if (bkg[i].isPlaying)
            {
                StartCoroutine(FadeAudioSource(bkg[i], bkgFadePoint));
            }
        }
    }

    public void RaiseBKG()
    {
        for (int i = 0; i < bkg.Length; i++)
        {
            if (bkg[i].isPlaying)
            {
                StartCoroutine(FadeAudioSource(bkg[i], 1));
            }
        }
    }

    public void StopBKG()
    {
        for (int i = 0; i < bkg.Length; i++)
        {
            if (bkg[i].isPlaying)
            {
                StartCoroutine(FadeAudioSource(bkg[i], 0));
            }
        }
    }

    private IEnumerator FadeAudioSource(AudioSource source, float targetVol) 
    {
        float curTime = 0;
        float start = source.volume;

        while(curTime < fadeDuration)
        {
            curTime += Time.deltaTime;
            source.volume = Mathf.Lerp(start, targetVol, curTime / fadeDuration);
            yield return null;
        }

        yield break;
    }


}
