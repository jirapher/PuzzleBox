using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{

    public int sceneToLoad = 0;
    private CamTransition cam;
    public string transitionName;

    private void Start()
    {
        cam = Camera.main.GetComponent<CamTransition>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            GameManager.instance.SetTransitionName(transitionName);
            StartCoroutine(cam.Fade(1, true, sceneToLoad));
        }
    }
}
