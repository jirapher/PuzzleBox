using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AudioManager audioMan;
    public CharController charController;
    public MusicScoreManager musicMan;
    public static GameManager instance;
    public Timer timer;
    public GameObject musicInterface;

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
        CloseMusicInterface();
        timer.StartTimer();
    }

    public void InstrumentUnlocked(int instrumentNum)
    {
        musicMan.UnlockInstrument(instrumentNum);
    }

    public void ResumeTimer()
    {
        timer.StartTimer();
    }

    public void PauseTimer()
    {
        timer.StopTimer();
    }

    public void StopBKG()
    {
        audioMan.StopBKG();
    }

    public void LowerBKG()
    {
        audioMan.LowerBKG();
    }

    public void RaiseBKG()
    {
        audioMan.RaiseBKG();
    }

    public void StartBKG()
    {
        audioMan.PlayBKG(Random.Range(0, 1));
    }


    #region AudioRecorder

    public void OpenMusicInterface()
    {
        if (musicInterface.activeInHierarchy) { CloseMusicInterface(); return; }
        LowerBKG();
        musicInterface.SetActive(true);
        musicMan.TurnOnMusicMan();
        charController.SetIgnoreInput();
        PauseTimer();
    }
    public void CloseMusicInterface()
    {
        RaiseBKG();
        musicInterface.SetActive(false);
        musicMan.TurnOffMusicMan();
        charController.IgnoreInputOff();
        ResumeTimer();
    }

    public void StartPlaying()
    {
        musicMan.StartPlaying();
    }

    public void StopPlaying()
    {
        musicMan.StopPlaying();
    }

    public void ClearAllMusic()
    {
        //warning message?
        musicMan.ClearAllNotes();
    }

    public CharController GetPlayer()
    {
        return charController;
    }


    #endregion
}
