using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtController : MonoBehaviour
{
    public float dirtAmount = 1;
    private float clean = .75f, dirty = 0f;
    private Material mat;

    public float adjAmt = 0.1f;
    public GameObject particles;
    public GameObject itemTospawn;

    private bool justBrushed = false;
    //public bool dirtGrowing = false;

    private void Start()
    {
        mat = GetComponent<SpriteRenderer>().material;
        dirtAmount = dirty;
        SetDirt();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Broom")
        {
            if (justBrushed) { return; }
            SubtractDirt();
        }
    }

    private void SubtractDirt()
    {
        dirtAmount += adjAmt;

        if(dirtAmount >= clean)
        {
            //it's clean
            Cleaned();
            return;
        }

        SetDirt();
        justBrushed = true;
        Invoke("BrushCooldown", .5f);
    }

    private void Cleaned()
    {
        particles.SetActive(true);
        SpawnPrize();
        Destroy(this.gameObject, 1.5f);
    }

    private void SetDirt()
    {
        mat.SetFloat("_DirtAmount", dirtAmount);
    }

    private void BrushCooldown()
    {
        justBrushed = false;
    }

    private void SpawnPrize()
    {
        Instantiate(itemTospawn, transform.position, Quaternion.identity, null);
    }
}
