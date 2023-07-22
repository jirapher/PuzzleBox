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

    private bool openMusicTipDisplayed = false;

    private string transitionName;
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
        CloseMusicOnStart();
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

    public void SetPlayerNotice(string notice)
    {
        timer.PlayerToolTip(notice);
    }

    public void SetTransitionName(string name)
    {
        transitionName = name;
    }

    public string GetTransitionName()
    {
        return transitionName;
    }


    #region AudioRecorder

    public void OpenMusicInterface()
    {
        if (!openMusicTipDisplayed)
        {
            SetPlayerNotice("Drag and drop instrument icons to build your song. Right click to remove notes.");
            openMusicTipDisplayed = true;
        }

        if (musicInterface.activeInHierarchy) { CloseMusicInterface(); return; }
        LowerBKG();
        musicInterface.SetActive(true);
        musicMan.TurnOnMusicMan();
        charController.SetIgnoreInput();
        PauseTimer();
        audioMan.PlaySFX(5);
    }
    public void CloseMusicInterface()
    {
        RaiseBKG();
        musicInterface.SetActive(false);
        musicMan.TurnOffMusicMan();
        charController.IgnoreInputOff();
        ResumeTimer();
        audioMan.PlaySFX(4);
    }

    private void CloseMusicOnStart()
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
