using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speaker : MonoBehaviour
{
    public string[] lines;
    public string charName;
    public Sprite charSprite;

    private bool canSpeak = false;
    private bool ignore = false;

    [Header("CS-Specific Portrait Swap")]
    public bool port1 = false;

    [Header("Instrument")]
    public bool isInstrument;

    private void Start()
    {
        if(charSprite == null)
        {
            charSprite = GetComponent<SpriteRenderer>().sprite;
        }
    }

    private void Update()
    {
        if (!canSpeak) { return; }

        if (Input.GetButtonUp("Fire1"))
        {
            if (DialogManager2.instance.dialogActive) { return; }

            if (ignore) { ignore = false; return; }

            if (isInstrument)
            {
                DialogManager2.instance.StartInstrumentDialog(lines, charSprite, charName, this.gameObject);
                this.enabled = false;
                return;
            }

            DialogManager2.instance.StartNewDialog(lines, charSprite, charName);
            ignore = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            canSpeak = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            canSpeak = false;
            ignore = false;
            DialogManager2.instance.EndDialog();
        }
    }
}
