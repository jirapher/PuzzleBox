using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class CamTransition : MonoBehaviour
{
    public SpriteRenderer blackFade;
    public float fadeDuration = 1f;
    public GameObject player;

    public string transitionName;
    public static CamTransition instance;

    public CinemachineConfiner2D camBounds;
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

        UpdateCamBounds();
        
        blackFade.color = Color.clear;
    }

    private void UpdateCamBounds()
    {
        camBounds.VirtualCamera.PreviousStateIsValid = false;
        camBounds.m_BoundingShape2D = GameObject.FindGameObjectWithTag("CamBounds").GetComponent<PolygonCollider2D>();
        camBounds.InvalidateCache();
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
        StartCoroutine(Fade(0, false, 0));
        player.GetComponent<CharController>().IgnoreInputOff();
    }
}
