using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtController : MonoBehaviour
{
    public float dirtAmount = 1;
    public float coverSpeed = 1;
    public float thresholdMin = 0.35f, thresholdMax = 1f;
    private Material mat;

    public bool dirtGrowing = false;

    private void Start()
    {
        mat = GetComponent<SpriteRenderer>().material;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            dirtGrowing = !dirtGrowing;
        }

        if (dirtGrowing) { StartDirt(); } else { StopDirt(); }

        mat.SetFloat("_DirtAmount", dirtAmount);
    }

    public void StartDirt()
    {
        if(dirtAmount > thresholdMin)
        {
            dirtAmount -= Time.deltaTime * coverSpeed;
        }
    }

    public void StopDirt()
    {
        if(dirtAmount < thresholdMax)
        {
            dirtAmount += Time.deltaTime * coverSpeed;
        }
    }
}
