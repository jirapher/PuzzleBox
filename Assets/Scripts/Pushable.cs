using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushable : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Push(Vector2 dir, float force)
    {
        rb.AddForce(force * dir, ForceMode2D.Impulse);
    }
}
