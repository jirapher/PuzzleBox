using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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

    }



}
