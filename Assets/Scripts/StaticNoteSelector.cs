using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticNoteSelector : MonoBehaviour
{
    public GameObject notePrefab;
    public GameObject highLight;

    private void Start()
    {
        highLight.SetActive(false);
    }

    public void SetHighlight()
    {
        highLight.SetActive(true);
    }

    public void HighlightOff()
    {
        highLight.SetActive(false);
    }

    public GameObject PopulateNote()
    {
        GameObject g = Instantiate(notePrefab, transform.position, Quaternion.identity, MusicScoreManager.instance.cloneHolder.transform);
        return g;
    }

    /*private void OnMouseEnter()
    {
        SetHighlight();
    }

    private void OnMouseExit()
    {
        HighlightOff();
    }*/

}
