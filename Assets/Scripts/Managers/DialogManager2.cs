using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class DialogManager2 : MonoBehaviour
{
    public static DialogManager2 instance;
    public TMP_Text curText, nameText;
    private int curLine;
    public string[] allLines;
    public Image curPortrait;
    public GameObject dialogPanel;
    public bool dialogActive = false;

    public CharController player;
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
        curText.text = "";
        curLine = 0;
    }

    private void Update()
    {
        if (!dialogActive) { return; }

        if (Input.GetButtonDown("Fire1"))
        {
            AdvanceDialog();
        }
    }

    public void StartNewDialog(string[] lines, Sprite pic, string name)
    {
        nameText.text = name;
        allLines = lines;
        curPortrait.sprite = pic;
        StartDialog();
        player.SetIgnoreInput();
    }

    public void StartCSDialog(string[] lines, Sprite pic, string name)
    {
        nameText.text = name;
        allLines = lines;
        curPortrait.sprite = pic;
        StartDialog();
    }

    private void StartDialog()
    {
        curLine = 0;
        curText.text = allLines[curLine];
        dialogPanel.SetActive(true);
        dialogActive = true;
        GameManager.instance.PauseTimer();
    }

    public void EndDialog()
    {
        curText.text = "";
        curLine = 0;
        allLines = new string[0];
        dialogActive = false;
        dialogPanel.SetActive(false);
        player.IgnoreInputOff();
        GameManager.instance.ResumeTimer();
    }

    public void AdvanceDialog()
    {
        //called through button
        if (!dialogActive) { return; }

        curLine++;

        if(curLine > allLines.Length - 1)
        {
            EndDialog();
            return;
        }

        curText.text = allLines[curLine];
    }
}
