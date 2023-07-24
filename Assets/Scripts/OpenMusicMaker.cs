using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMusicMaker : MonoBehaviour
{
    private bool canOpen = false;
    private bool opened = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            canOpen = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        canOpen = false;
        opened = false;
    }

    private void Update()
    {
        if (!canOpen || opened) { return; }

        if (Input.GetKeyDown(KeyCode.Mouse0) && !MusicScoreManager.instance.musicMenuUp)
        {
            GameManager.instance.OpenMusicInterface();
            opened = true;
        }
    }
}
