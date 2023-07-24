using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviour
{
    public DialogManager2 dm;

    public Speaker[] speakers;
    private int speakerCount = 0;

    [Header("CS Config")]
    public GameObject[] offAtStart;
    private void Start()
    {
        UnnecessaryObjOff();
        speakerCount = speakers.Length;
        dm.gameObject.SetActive(true);
        StartCoroutine(StartDialogChain());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            BruteForce();
        }
    }

    private void UnnecessaryObjOff()
    {
        for (int i = 0; i < offAtStart.Length; i++)
        {
            offAtStart[i].gameObject.SetActive(false);
        }
    }

    private void UnnecessaryObjOn()
    {
        for (int i = 0; i < offAtStart.Length; i++)
        {
            offAtStart[i].gameObject.SetActive(true);
        }
    }

    private IEnumerator StartDialogChain()
    {
        
        int curSpeaker = 0;

        while(curSpeaker < speakerCount)
        {
            //SetCam();
            while(dm.dialogActive) { yield return null; }

            dm.StartCSDialog(speakers[curSpeaker].lines, speakers[curSpeaker].charSprite, speakers[curSpeaker].charName, speakers[curSpeaker].port1);

            curSpeaker++;

            yield return null;
        }


        while (dm.dialogActive)
        {
            yield return null;
        }

        DialogManager2.instance.csDialog = false;
        UnnecessaryObjOn();
        SceneManager.LoadScene(5);

    }

    private void SetCam()
    {
        Camera.main.GetComponentInChildren<CinemachineVirtualCamera>().Follow = this.transform;
        Camera.main.GetComponentInChildren<CinemachineVirtualCamera>().LookAt = this.transform;
    }

    private void BruteForce()
    {
        DialogManager2.instance.csDialog = false;
        UnnecessaryObjOn();
        SceneManager.LoadScene(5);
    }




}
