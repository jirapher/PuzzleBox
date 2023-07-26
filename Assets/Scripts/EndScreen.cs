using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndScreen : MonoBehaviour
{

    public TMP_Text dialogTxt;

    private void Start()
    {
        MusicScoreManager.instance.FinalActivation();
        dialogTxt.text = MusicScoreManager.instance.ScoreCalc();
        Object.FindObjectOfType<CharController>().gameObject.SetActive(false);
    }


    public void QuitApp()
    {
        Application.Quit();
    }

    public void ToBeginning()
    {
        DeleteAll();
        SceneManager.LoadScene(0);
    }

    private void DeleteAll()
    {
        foreach (GameObject g in Object.FindObjectsOfType<GameObject>())
        {
            if (g != this)
            {
                Destroy(g);
            }
        }

    }
}
