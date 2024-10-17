using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlimeSpawner : MonoBehaviour
{
    public GameObject SlimePath;
    public int SlimeLength;

    private List<GameObject> pathPieces = new List<GameObject>();

    public void MakePath(Vector3 pos)
    {
        GameObject path = Instantiate(SlimePath, transform.parent);
        path.transform.position = new Vector3(pos.x, SlimePath.transform.position.y, pos.z);
        pathPieces.Add(path);
    }
    public void Update()
    {
        PrunePath();
    }
    public void LateUpdate()
    {
        UpdatePath();
    }

    public void UpdatePath()
    {
        if (pathPieces.Count > SlimeLength)
        {
            pathPieces[0].GetComponent<SlimePathData>().Kill();
            pathPieces.RemoveAt(0);
        }

        UpdatePathIndex();
    }

    public void UpdatePathIndex()
    {
        for (int i = pathPieces.Count - 1; i > -1; i--)
        {
            pathPieces[i].GetComponent<SlimePathData>().index = pathPieces.Count - i;
            pathPieces[i].GetComponent<SlimePathData>().max = pathPieces.Count;
        }
    }

    public void PrunePath()
    {
        //Prune for null references
        for (int i = pathPieces.Count - 1; i > -1; i--)
        {
            if (pathPieces[i] != pathPieces[i].GetComponent<SlimePathData>().alive)
                pathPieces.RemoveAt(i);
        }
    }

}
