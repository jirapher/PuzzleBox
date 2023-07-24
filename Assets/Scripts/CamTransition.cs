using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class CamTransition : MonoBehaviour
{
    public SpriteRenderer blackFade;
    public float fadeDuration = 1f;

    public string transitionName;
    public static CamTransition instance;
    public CinemachineVirtualCamera vcam;

    //public CinemachineConfiner2D camBounds;
    private void Start()
    {
        blackFade.color = Color.black;
        FadeIn();
        vcam.Follow = GameManager.instance.GetPlayer().gameObject.transform;
        vcam.LookAt = GameManager.instance.GetPlayer().gameObject.transform;
    }

    /*public void UpdateCamBounds()
    {
        camBounds.VirtualCamera.PreviousStateIsValid = false;
        camBounds.m_BoundingShape2D = GameObject.FindGameObjectWithTag("CamBounds").GetComponent<PolygonCollider2D>();
        camBounds.InvalidateCache();
    }*/

    public IEnumerator Fade(float fadeTo, bool shouldLoadSceen, int sceneToLoad)
    {
        //player.GetComponent<CharController>().SetIgnoreInput();
        GameManager.instance.GetPlayer().SetIgnoreInput();
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
        GameManager.instance.GetPlayer().gameObject.transform.position = FindObjectOfType<PlayerPosOnLoad>().transform.position;
        StartCoroutine(Fade(0, false, 0));
        GameManager.instance.GetPlayer().IgnoreInputOff();
    }
}
