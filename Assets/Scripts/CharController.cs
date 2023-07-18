using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    private Vector2 moveInput = Vector2.zero;
    private Vector2 lastMoveInput = Vector2.zero;
    private Rigidbody2D rb;

    private Animator anim;
    private string inputX = "InputX", inputY = "InputY", lastX = "LastMoveX", lastY = "LastMoveY";
    private bool walking = false;

    private bool atkCoolDown = false;
    private bool ignoreInput = false;

    public static CharController instance;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
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
        if(ignoreInput || atkCoolDown) { return; }
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnMove(InputValue value)
    {
        if (ignoreInput) { return; }

        moveInput = value.Get<Vector2>();

        if(moveInput == lastMoveInput) { return; }

        if (moveInput == Vector2.zero) { walking = false; } else { walking = true; }

        lastMoveInput = moveInput;

        UpdateAnim();
    }

    private void UpdateAnim()
    {
        anim.SetFloat(inputX, moveInput.x);
        anim.SetFloat(inputY, moveInput.y);
        anim.SetBool("Walking", walking);

        if(lastMoveInput == Vector2.zero) { return; }

        anim.SetFloat(lastX, moveInput.x);
        anim.SetFloat(lastY, moveInput.y);
    }

    void OnFire()
    {
        //if (atkCoolDown || ignoreInput) { return; }

        anim.SetTrigger("Attack");
        atkCoolDown = true;
        Invoke("AttackCooldown", 0.8f);
    }

    private void AttackCooldown()
    {
        atkCoolDown = false;
    }

    public void SetIgnoreInput()
    {
        moveInput = Vector2.zero;
        anim.SetBool("Walking", false);
        ignoreInput = true;
    }

    public void IgnoreInputOff()
    {
        ignoreInput = false;
    }
}
