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


    #region Audio

    public void OpenMusicInterface()
    {
        if (musicInterface.activeInHierarchy) { musicInterface.SetActive(false); musicMan.TurnOffMusicMan(); return; }
        musicInterface.SetActive(true);
        musicMan.TurnOnMusicMan();
        charController.SetIgnoreInput();
        PauseTimer();
    }
    public void CloseMusicInterface()
    {
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
