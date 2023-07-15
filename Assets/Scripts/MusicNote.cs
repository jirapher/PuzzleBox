using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicNote : MonoBehaviour
{
    private int noteNum;
    public int[] possibleNotes;
    public Color mouseOverColor = Color.red;
    private Color originalColor;
    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
        noteNum = possibleNotes[0];
    }

    public void PlayClip()
    {
        AudioManager.instance.PlayIndexSound(noteNum);
    }

    public void SwitchNote(int newNote)
    {
        noteNum = possibleNotes[newNote];
    }
}
