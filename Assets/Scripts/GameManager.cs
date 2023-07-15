using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AudioManager audioMan;
    //public InputRecorder recorder;
    public CharController charController;
    public MusicScoreManager musicMan;
    public static GameManager instance;

    public GameObject musicInterface;
    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        CloseMusicInterface();
    }

    public void InstrumentUnlocked(int instrumentNum)
    {
        musicMan.UnlockInstrument(instrumentNum);
    }


    #region Audio

    public void OpenMusicInterface()
    {
        if (musicInterface.activeInHierarchy) { musicInterface.SetActive(false); musicMan.TurnOffMusicMan(); return; }
        musicInterface.SetActive(true);
        musicMan.TurnOnMusicMan();
    }
    public void CloseMusicInterface()
    {
        musicInterface.SetActive(false);
        musicMan.TurnOffMusicMan();
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


    #endregion
}
