using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicNote : MonoBehaviour
{
    private int noteNum;
    public int[] possibleNotes;

    private void Start()
    {
        noteNum = possibleNotes[0];
    }

    public void PlayClip()
    {
        AudioManager.instance.PlayIndexSound(noteNum);
    }

    public void SwitchNote(int newNote)
    {
        noteNum = possibleNotes[newNote];
        PlayClip();
    }
}
