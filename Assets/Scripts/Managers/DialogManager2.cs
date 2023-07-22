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

    public bool csDialog = false;
    public CharController player;
    public GameObject portPos1, portPos2;

    //Portrait2
    public Image curPortrait2;
    public TMP_Text port2Name;


    private GameObject unlockInstrumentObj;
    private bool instrumentDialog = false;
    public Image instrumentPortrait;
    public TMP_Text instrumentName;

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
        portPos2.SetActive(false);
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

    public void StartInstrumentDialog(string[] lines, Sprite pic, string name, GameObject instrument)
    {
        unlockInstrumentObj = instrument;
        instrumentDialog = true;
        instrumentName.text = name;
        allLines = lines;
        instrumentPortrait.sprite = pic;
        StartDialog();
        player.SetIgnoreInput();
    }

    public void StartCSDialog(string[] lines, Sprite pic, string name, bool atPort1)
    {
        csDialog = true;

        allLines = lines;

        if (!atPort1)
        {
            port2Name.text = name;
            curPortrait2.sprite = pic;
            portPos2.SetActive(true);
            portPos1.SetActive(false);
        }
        else
        {
            portPos1.SetActive(true);
            portPos2.SetActive(false);
            nameText.text = name;
            curPortrait.sprite = pic;
        }

        StartDialog();
    }

    private void StartDialog()
    {
        curLine = 0;
        curText.text = allLines[curLine];
        if (instrumentDialog)
        {
            instrumentPortrait.gameObject.SetActive(true);
            curPortrait.gameObject.SetActive(false);
        }

        dialogPanel.SetActive(true);
        dialogActive = true;
        GameManager.instance.PauseTimer();
    }

    public void EndDialog()
    {
        if (instrumentDialog)
        {
            Destroy(unlockInstrumentObj);
            instrumentPortrait.gameObject.SetActive(false);
            curPortrait.gameObject.SetActive(true);
        }

        instrumentDialog = false;
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
