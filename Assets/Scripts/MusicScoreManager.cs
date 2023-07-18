using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MusicScoreManager : MonoBehaviour
{
    public bool musicMenuUp = false;
    public CharController player;

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
            //Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 newPos = Input.mousePosition;
            //newPos.z = 0;
            draggedItem.transform.position = newPos;
        }

        if (Input.GetMouseButtonUp(0) && dragging)
        {
            draggedItem.transform.position = new Vector3(draggedItem.transform.position.x, draggedItem.transform.position.y, 0);
            draggedItem = null;
            dragging = false;
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
    }

    public void StartPlaying()
    {
        scoreAnim.speed = 1;
    }

    public void TurnOnMusicMan()
    {
        musicMenuUp = true;
        player.SetIgnoreInput();
        if(allCurrentNotes.Count > 0)
        {
            foreach(GameObject n in allCurrentNotes)
            {
                n.gameObject.SetActive(true);
            }
        }
    }

    public void TurnOffMusicMan()
    {
        player.IgnoreInputOff();

        if (allCurrentNotes.Count > 0)
        {
            foreach (GameObject n in allCurrentNotes)
            {
                n.gameObject.SetActive(false);
            }
        }

        musicMenuUp = false;

    }

    public void UnlockInstrument(int instrumentNum)
    {
        Instantiate(allPopulators[instrumentNum], populatorPositions[nextPopulatorPosition].position, Quaternion.identity, populatorPositions[nextPopulatorPosition]);
        nextPopulatorPosition++;
    }

}
