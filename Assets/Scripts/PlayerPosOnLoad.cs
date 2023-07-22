using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPosOnLoad : MonoBehaviour
{

    public string transitionName;

    private void Start()
    {

        if(GameManager.instance.GetTransitionName() == transitionName)
        {
            FindObjectOfType<CharController>().transform.position = this.transform.position;
        }
    }
}
