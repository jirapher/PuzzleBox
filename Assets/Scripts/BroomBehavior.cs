using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroomBehavior : MonoBehaviour
{

    public float pushForce;
    private Vector2 pushDir = Vector2.zero;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Pushable")
        {
            pushDir = other.transform.position - transform.position;
            other.GetComponent<Pushable>().Push(pushDir, pushForce);
        }
    }

    private Vector2 RotCheck()
    {
        float z = transform.rotation.z;

        switch (z)
        {
            case 180:
                //Up
                return pushDir = Vector2.up;

            case 0:
                //Down
                return pushDir = Vector2.down;

            case -90:
                //Left
                return pushDir = Vector2.left;

            case 90:
                //Right
                return pushDir = Vector2.right;
        }

        return Vector2.zero;
    }
}
