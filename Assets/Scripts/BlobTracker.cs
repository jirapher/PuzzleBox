using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobTracker : MonoBehaviour
{
    public static BlobTracker instance;

    public List<string> BlobIDs;

    private void Start()
    {
        instance = this;
    }

    public void AddBlob(string blobID)
    {
        BlobIDs.Add(blobID);
    }

    public bool CheckBlob(string blobID)
    {
        //if true, blob has been used
        if (BlobIDs.Contains(blobID)) { return true; }

        return false;
    }
}
