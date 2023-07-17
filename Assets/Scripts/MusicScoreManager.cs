using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScoreManager : MonoBehaviour
{
    public bool musicMenuUp = false;

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


    private void Start()
    {
        instance = this;
        musicMenuUp = false;
    }
    private void Update()
    {
        if (!musicMenuUp) { return; }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D ray = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition), 10f, selectableLayer);
            string tag = ray.transform.tag;

            if (ray.transform.tag == "Note")
            {
                SetNoteDrag(ray.transform.gameObject);
            }

            if (ray.transform.tag == "Populator")
            {
                PopulateNote(ray.transform.gameObject);
            }

        }

        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit2D ray = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition), 10f, selectableLayer);

            if (ray.transform.tag == "Note")
            {
                DeleteNote(ray.transform.gameObject);
            }
        }

        if(Input.GetMouseButton(0) && dragging)
        {
            if(draggedItem == null) { return; }
            Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            newPos.z = 0;
            draggedItem.transform.position = newPos;
        }

        if (Input.GetMouseButtonUp(0) && dragging)
        {
            draggedItem.transform.position = new Vector3(draggedItem.transform.position.x, draggedItem.transform.position.y, 0);
            draggedItem = null;
            dragging = false;
        }
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
