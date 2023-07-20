using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawing : MonoBehaviour
{
    private Camera cam;
    public GameObject brush;

    LineRenderer curLineRender;

    public Transform brushPos;

    Vector2 lastPos;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        DrawLine();
    }

    void DrawLine()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            CreateBrush();
        }

        if (Input.GetKey(KeyCode.C))
        {
            //holding
            Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            if(mousePos != lastPos)
            {
                AddPoint(mousePos);
                lastPos = mousePos;
            }
            
        }
        else
        {
            curLineRender = null;
        }
    }

    void CreateBrush()
    {
        GameObject brushInstance = Instantiate(brush);
        curLineRender = brushInstance.GetComponent<LineRenderer>();

        Vector2 bp = cam.ScreenToWorldPoint(Input.mousePosition);

        curLineRender.SetPosition(0, bp);
        curLineRender.SetPosition(1, bp);

    }

    void AddPoint(Vector2 pointPos)
    {
        GameObject brushInstance = Instantiate(brush);
        curLineRender.positionCount++;
        int posIndex = curLineRender.positionCount - 1;
        curLineRender.SetPosition(posIndex, pointPos);
    }
}
