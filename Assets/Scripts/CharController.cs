using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    private Vector2 moveInput = Vector2.zero;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    /*private void Update()
    {
        if(Input.mouseScrollDelta.y != 0)
        {
            ReadScroll();
        }
    }*/

    private void ReadScroll()
    {
        float v = Input.mouseScrollDelta.y;
        if(v > 0) { AudioManager.instance.PitchUp(); } else { AudioManager.instance.PitchDown(); }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnFire()
    {
        //print("Shot fired.");
    }
}
