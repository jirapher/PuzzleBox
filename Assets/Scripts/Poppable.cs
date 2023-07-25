using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poppable : MonoBehaviour
{
    public GameObject particles;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Broom")
        {
            particles.SetActive(true);
            Destroy(gameObject, 1f);
        }
    }
}
