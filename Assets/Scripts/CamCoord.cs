using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CamCoord : MonoBehaviour
{
    public TMP_Text coordTxt;

    private void Update()
    {
        coordTxt.text = gameObject.transform.position.ToString();
    }
}
