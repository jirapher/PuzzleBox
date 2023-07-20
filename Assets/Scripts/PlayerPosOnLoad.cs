using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPosOnLoad : MonoBehaviour
{

    public string transitionName;

    private void Start()
    {
        //CamTransition cam = Camera.main.GetComponent<CamTransition>();

        /*if(cam.transitionName == transitionName)
        {
            cam.FadeIn();
            FindObjectOfType<CharController>().transform.position = this.transform.position;
        }*/
    }
}
