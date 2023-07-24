using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotePicker : MonoBehaviour
{
    public int pickerNum = 0;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Note")
        {
            other.GetComponent<MusicNote>().SwitchNote(pickerNum);
        }
    }
}
