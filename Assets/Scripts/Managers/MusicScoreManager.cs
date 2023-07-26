using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MusicScoreManager : MonoBehaviour
{
    public bool musicMenuUp = false;

    [Header("NoticePanel")]
    public GameObject musicNoticePanel;
    public TMP_Text noticeTxt;
    public Button finalYes, finalNo;

    [Header("Note Handling")]
    public LayerMask selectableLayer;
    public bool dragging = false;
    private GameObject draggedItem = null;
    public static MusicScoreManager instance;
    public Animator scoreAnim;
    public List<GameObject> allCurrentNotes = new List<GameObject>();
    public GameObject cloneHolder;

    [Header("Populators")]
    public GameObject[] allPopulators;
    public bool[] populatorUnlock;
    public Transform[] populatorPositions;
    private int nextPopulatorPosition = 0;

    [Header("Raycast Redo")]
    public PointerEventData ped;
    public GraphicRaycaster ray;
    public EventSystem events;

    [Header("EndBit")]
    public GameObject[] bitsToKeepOn;
    public GameObject[] bitsToTurnOff;

    private void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }

        InitPopUnlock();

        musicNoticePanel.SetActive(false);
        musicMenuUp = false;
    }
    private void Update()
    {
        if (!musicMenuUp) { return; }

        if (Input.GetMouseButtonDown(0))
        {
            
            foreach(RaycastResult r in UIRaycast())
            {
                if(r.gameObject.tag == "Note")
                {
                    SetNoteDrag(r.gameObject);
                }

                if (r.gameObject.tag == "Populator")
                {
                    PopulateNote(r.gameObject);
                }
            }

        }

        if (Input.GetMouseButtonDown(1))
        {

            foreach (RaycastResult r in UIRaycast())
            {
                if (r.gameObject.tag == "Note")
                {
                    DeleteNote(r.gameObject);
                }
            }
        }

        if(Input.GetMouseButton(0) && dragging)
        {
            if(draggedItem == null) { return; }
            Vector3 newPos = Input.mousePosition;
            draggedItem.transform.position = newPos;
        }

        if (Input.GetMouseButtonUp(0) && dragging)
        {
            draggedItem.transform.position = new Vector3(draggedItem.transform.position.x, draggedItem.transform.position.y, 0);
            draggedItem = null;
            dragging = false;
        }
    }

    private void InitPopUnlock()
    {
        populatorUnlock = new bool[allPopulators.Length];

        for(int i = 0; i < populatorUnlock.Length; i++)
        {
            populatorUnlock[i] = false;
        }
    }



    private List<RaycastResult> UIRaycast()
    {
        ped = new PointerEventData(events);
        ped.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        ray.Raycast(ped, results);
        return results;
    }

    private void SetNoteDrag(GameObject note)
    {
        draggedItem = note;
        dragging = true;
    }

    private void PopulateNote(GameObject populator)
    {
        draggedItem = populator.GetComponent<StaticNoteSelector>().PopulateNote();
        AddNote(draggedItem);
        dragging = true;
    }

    public void ClearAllNotes()
    {
        foreach (GameObject n in allCurrentNotes)
        {
            Destroy(n);
        }

        allCurrentNotes.Clear();

        scoreAnim.SetTrigger("Reset");
    }

    public void DeleteNote(GameObject note)
    {
        for(int i = 0; i < allCurrentNotes.Count; i++)
        {
            if(note == allCurrentNotes[i])
            {
                Destroy(allCurrentNotes[i]);
                allCurrentNotes.Remove(allCurrentNotes[i]);
            }
        }
    }

    public void AddNote(GameObject note)
    {
        allCurrentNotes.Add(note);
    }

    public void StopPlaying()
    {
        scoreAnim.speed = 0;
        ResetAnim();
    }

    public void ResetAnim()
    {
        scoreAnim.SetTrigger("Reset");
    }

    public void StartPlaying()
    {
        scoreAnim.SetTrigger("Play");
        scoreAnim.speed = 1;
    }

    public void TurnOnMusicMan()
    {
        musicMenuUp = true;
        if(allCurrentNotes.Count > 0)
        {
            foreach(GameObject n in allCurrentNotes)
            {
                n.gameObject.SetActive(true);
            }
        }

        scoreAnim.SetTrigger("Play");
    }

    public void TurnOffMusicMan()
    {

        if (allCurrentNotes.Count > 0)
        {
            foreach (GameObject n in allCurrentNotes)
            {
                n.gameObject.SetActive(false);
            }
        }

        musicMenuUp = false;
        scoreAnim.SetTrigger("Play");
    }

    public void FinalActivation()
    {
        if (allCurrentNotes.Count > 0)
        {
            foreach (GameObject n in allCurrentNotes)
            {
                n.gameObject.SetActive(true);
            }
        }

        for(int i = 0; i < bitsToTurnOff.Length; i++)
        {
            bitsToTurnOff[i].SetActive(false);
        }

        for(int i = 0; i < bitsToKeepOn.Length; i++)
        {
            bitsToKeepOn[i].SetActive(true);
        }

        scoreAnim.SetTrigger("Play");
    }

    public void UnlockInstrument(int instrumentNum)
    {
        if (populatorUnlock[instrumentNum] == true) { return; }

        Instantiate(allPopulators[instrumentNum], populatorPositions[nextPopulatorPosition].position, Quaternion.identity, populatorPositions[nextPopulatorPosition]);
        populatorUnlock[instrumentNum] = true;
        nextPopulatorPosition++;
    }

    public void SetMusicNotice(string notice)
    {
        noticeTxt.text = notice;
        musicNoticePanel.SetActive(true);
        Invoke("NoticeOff", 4f);
    }

    public void SetFinalNotice()
    {
        //set buttons active
        finalYes.gameObject.SetActive(true);
        finalNo.gameObject.SetActive(true);
        noticeTxt.text = "Are you sure you're ready to submit? This is what will usher in a new world.";
        musicNoticePanel.SetActive(true);
    }

    public void FinalYes()
    {
        //load scene
        SceneManager.LoadScene(6);
    }

    public void FinalNo()
    {
        musicNoticePanel.SetActive(false);
        finalYes.gameObject.SetActive(false);
        finalNo.gameObject.SetActive(false);
    }

    private void NoticeOff()
    {
        musicNoticePanel.SetActive(false);
    }


    public void PlaySong()
    {
        GameManager.instance.StartPlaying();
    }

    public void StopSong()
    {
        GameManager.instance.StopPlaying();
    }

    public void ClearSong()
    {
        GameManager.instance.ClearAllMusic();
    }

    public void FinishedMusic()
    {
        GameManager.instance.CloseMusicInterface();
    }

    public void FinalSubmission()
    {
        SetFinalNotice();
    }

    public string ScoreCalc()
    {
        int instrumentsFound = 0;
        int notesUsed = 0;

        for(int i = 0; i < populatorUnlock.Length; i++)
        {
            if (populatorUnlock[i] == true)
            {
                instrumentsFound++;
            }
        }

        notesUsed = allCurrentNotes.Count;

        if(notesUsed >= 15)
        {
            return ReturnCommentOne();
        }

        if(notesUsed >= 20)
        {
            return ReturnCommentTwo();
        }

        if(instrumentsFound > 5 && notesUsed > 5)
        {
            return ReturnCommentThree();
        }

        return ReturnCommentFour();

    }

    private string ReturnCommentOne()
    {
        //all instruments
        return "HOT DANG! Look at you finding all the instruments and saving all the musics. Future Earth is sure to be full of liberal arts majors.";
    }

    private string ReturnCommentTwo()
    {
        //>20 notes
        return "Wow! You really mashed a lot of notes in your score. You're a regular ol' Wolfgang Mozart!";
    }

    private string ReturnCommentThree()
    {
        //<20 notes
        return "Huh. Ya know, an effort was made. Your new wave of people are sure to enjoy avant-garde experimental music.";
    }

    private string ReturnCommentFour()
    {
        //lackluster
        return "Hmm... Art (or lack thereof) is still art I guess. Earth 2 might be a little more quiet.";
    }





}
