using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Ink.Runtime;
using UnityEngine.UI;
using TMPro;

public class DialogManager : MonoBehaviour
{
    /*
    public static DialogManager instance;

    public TextAsset inkFile;

    public GameObject button;

    public GameObject textPanel;

    public GameObject optionPanel;

    public Image portrait;
    public Sprite[] possiblePortraits;

    public bool isTalking = false;
    
    //static Story story;

    public TMP_Text nametag;
    public TMP_Text message;

    List<string> tags = new List<string>();
    //static Choice choiceSelected = null;

    private void Start()
    {
        instance = this;
        DontDestroyOnLoad(this);
        //story = new Story(inkFile.text);
    }

    private void Update()
    {
        //this is thrown in update??
        if (story.canContinue)
        {
            //nametag.text = new name
            AdvanceDialog();

            if(story.currentChoices.Count != 0)
            {
                StartCoroutine(ShowChoices());
            }
        }
        else
        {
            FinishDialog();
        }
    }

    private void FinishDialog()
    {
        print("Dialog end.");
    }

    private void AdvanceDialog()
    {
        string curSentence = story.Continue();
        ParseTags();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(message.text));
    }

    private void ParseTags()
    {
        //enabled with #tag in editor
        //idea is to feed numbers through "Portrait 0" and plug them into the array
        //great for portrait swaps
        tags = story.currentTags;

        foreach(string t in tags)
        {
            string prefix = t.Split(' ')[0];
            string param = t.Split(' ')[1];

            switch (prefix.ToLower())
            {
                case "Portrait":
                    int val;
                    int.TryParse(param, out val);
                    portrait.sprite = possiblePortraits[val];
                    break;

                    //do we need more tags?
            }
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        message.text = "";

        foreach(char letter in sentence.ToCharArray())
        {
            message.text += letter;
            yield return null;
        }

    }

    IEnumerator ShowChoices()
    {
        List<Choice> choices = story.currentChoices;

        for(int i = 0; i < choices.Count; i++)
        {
            GameObject temp = Instantiate(button, optionPanel.transform);
            temp.transform.GetChild(0).GetComponent<TMP_Text>().text = choices[i].text;

            temp.AddComponent<DialogButton>();
            temp.GetComponent<DialogButton>().element = choices[i];
            temp.GetComponent<Button>().onClick.AddListener(() => { temp.GetComponent<DialogButton>().Decide(); });
        }

        optionPanel.SetActive(true);

        yield return new WaitUntil(() => { return choiceSelected != null; });

        AdvanceFromDecision();
    }

    //tells the story what branch to go into
    public void SetDecision(object element)
    {
        choiceSelected = (Choice)element;
        story.ChooseChoiceIndex(choiceSelected.index);
    }

    public void AdvanceFromDecision()
    {
        optionPanel.SetActive(false);
        for(int i = 0; i < optionPanel.transform.childCount; i++)
        {
            Destroy(optionPanel.transform.GetChild(i).gameObject);
        }

        choiceSelected = null;
        AdvanceDialog();
    }
    */
}
