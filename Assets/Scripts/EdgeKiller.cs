using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeKiller : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Note")
        {
            MusicScoreManager.instance.DeleteNote(other.gameObject);
        }
    }
}
