using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaBox : MonoBehaviour
{
    public GameObject flame;
    private SpriteRenderer sr;
    private int ignitionNum = 0;
    public int ignitionThreshold = 2;
    public GameObject token;

    private void Start()
    {
        flame.SetActive(false);
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Broom")
        {
            ignitionNum++;
            if(ignitionNum > ignitionThreshold)
            {
                FlameSequence();
            }
        }
    }

    void FlameSequence()
    {
        flame.SetActive(true);
        StartCoroutine(Fade(2f));
        Destroy(gameObject, 2.25f);
    }

    public IEnumerator Fade(float fadeDuration)
    {
        Color initCol = sr.color;
        Color targetCol = Color.black;

        float elapsedTime = 0;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            sr.color = Color.Lerp(initCol, targetCol, elapsedTime / fadeDuration);
            yield return null;
        }

        if(token == null) { yield break; }
        Instantiate(token, transform.position, Quaternion.identity, null);
    }
}
