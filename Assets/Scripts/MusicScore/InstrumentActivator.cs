using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrumentActivator : MonoBehaviour
{
    public int instrumentNum = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            GameManager.instance.InstrumentUnlocked(instrumentNum);
        }
    }
}
