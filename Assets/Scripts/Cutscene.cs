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
            while (dm.dialogActive) { yield return null; }

            dm.StartCSDialog(speakers[curSpeaker].lines, speakers[curSpeaker].charSprite, speakers[curSpeaker].charName);

            curSpeaker++;

            yield return null;
        }


        while (dm.dialogActive)
        {
            yield return null;
        }

        UnnecessaryObjOn();
        SceneManager.LoadScene(5);

    }




}
