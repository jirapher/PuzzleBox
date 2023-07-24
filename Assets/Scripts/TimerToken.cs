using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerToken : MonoBehaviour
{
    public float timeToAdd;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            GameManager.instance.timer.AddTime(timeToAdd);
            AudioManager.instance.PlaySFX(6);
            Destroy(gameObject);
        }
    }
}
