using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CamTransition : MonoBehaviour
{
    public SpriteRenderer blackFade;
    public float fadeDuration = 1f;
    public GameObject player;

    public string transitionName;
    public static CamTransition instance;
    private void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this);
            instance = this;
        }
        blackFade.color = Color.clear;
    }

    public IEnumerator Fade(float fadeTo, bool shouldLoadSceen, int sceneToLoad)
    {
        player.GetComponent<CharController>().SetIgnoreInput();
        Color initCol = blackFade.color;
        Color targetCol = new Color(initCol.r, initCol.g, initCol.b, fadeTo);

        float elapsedTime = 0;

        while(elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            blackFade.color = Color.Lerp(initCol, targetCol, elapsedTime / fadeDuration);
            yield return null;
        }

        if (shouldLoadSceen)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    public void FadeIn()
    {
        print("fade in called");
        StartCoroutine(Fade(0, false, 0));
        player.GetComponent<CharController>().IgnoreInputOff();
    }
}
