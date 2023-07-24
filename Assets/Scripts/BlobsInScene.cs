using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobsInScene : MonoBehaviour
{
    public GameObject[] blobsInScene;

    private void Start()
    {
        foreach(GameObject blob in blobsInScene)
        {
            if (BlobTracker.instance.CheckBlob(blob.name))
            {
                Destroy(blob);
            }
        }
    }
}
