using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtController : MonoBehaviour
{

    //Instead start at .75 and work down to zero.

    public float dirtAmount = 1;
    private float clean = 0f, dirty = 0.8f;
    private Material mat;

    public float adjAmt = 0.1f;
    public GameObject particles;
    public GameObject itemTospawn;

    private bool justBrushed = false;
    private SpriteRenderer sr;
    //public bool dirtGrowing = false;
    private bool cleaned = false;

    [Header("MovingValues")]
    private Vector2 texOffset;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        mat = sr.material;
        dirtAmount = dirty;
        SetDirt();
        //StartCoroutine(RunShadeAdj());
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
        dirtAmount -= adjAmt;

        if(dirtAmount <= clean)
        {
            //it's clean
            Cleaned();
            return;
        }

        SetDirt();
        justBrushed = true;
        Invoke("BrushCooldown", .5f);
    }

    /*private IEnumerator RunShadeAdj()
    {
        bool go = true;
        float t = Random.Range(0.1f, 0.5f);

        while(go)
        {
            t -= Time.deltaTime;
            if(t <= 0)
            {
                ShaderAdjust();
                t = Random.Range(0.1f, 0.5f);
                yield return null;
            }

            yield return null;
        }
    }

    private void ShaderAdjust()
    {
        Vector2 adj = new Vector2(Random.Range(0.05f, -0.05f), Random.Range(0.05f, -0.05f));

        texOffset += adj;
        //vec2
        mat.SetVector("_DirtMapping", texOffset);
        ScaleAdj(adj);
    }

    private void ScaleAdj(Vector2 adj)
    {
        Transform t = gameObject.transform;
        t.localScale = new Vector3(t.localScale.x + adj.x, t.localScale.y + adj.y, t.localScale.z);
        gameObject.transform.localScale = t.localScale;
    }*/

    private void Cleaned()
    {
        if (cleaned) { return; }
        cleaned = true;
        SpawnPrize();
        StartCoroutine(FadeSprite());
        particles.SetActive(true);
        BlobTracker.instance.AddBlob(gameObject.name);
        AudioManager.instance.PlaySFX(3);
        Destroy(this.gameObject, 1.5f);
    }

    private IEnumerator FadeSprite()
    {
        float alpha = sr.color.a;
        for(float t = 0; t < 1f; t += Time.deltaTime / 1.3f)
        {
            Color newCol = new Color(1, 1, 1, Mathf.Lerp(alpha, 0, t));
            sr.color = newCol;
            yield return null;
        }
    }

    private void SetDirt()
    {
        mat.SetFloat("_DirtAmount", dirtAmount);
        texOffset = mat.GetVector("_DirtMapping");
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
