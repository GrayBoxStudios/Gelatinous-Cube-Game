using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateFloor : MonoBehaviour
{
    public Transform Cam;
    public GameObject floorTile;

    Vector3 _startDrag;
    Vector3 _endDrag;
    Ray _camPoint;

    void Update()
    {
        _camPoint = Cam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            if(_startDrag == Vector3.zero)
            {
                Ray ray = _camPoint;
                RaycastHit hit;
                Physics.Raycast(ray, out hit, 100f);
                _startDrag = hit.point;
            }
        }

        //if (_startDrag != Vector3.zero)
        {
            Ray ray = _camPoint;
            RaycastHit hit;
            Physics.Raycast(ray, out hit, 100f);
            _endDrag = hit.point;
        }

        if (Input.GetMouseButtonUp(0))
        {
            BuildFloor();
        }

    }

    void BuildFloor()
    {
        int Rows = Mathf.Abs((int)(_startDrag.x / _endDrag.x));
        int Cols = Mathf.Abs((int)(_startDrag.z / _endDrag.z));

        Vector3 roundedStart = new Vector3(Mathf.Round(_startDrag.x / 2) * 2, 0f, Mathf.Round(_startDrag.z / 2) * 2);

        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Cols; j++)
            {
                GameObject tile = Instantiate(floorTile);
                tile.transform.position = new Vector3(i * 2f, -1.5f, j * 2f);
                tile.transform.position += roundedStart;
            }
        }

        _startDrag = Vector3.zero;
        _endDrag = Vector3.zero;

    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawSphere(_endDrag, 1f);

        Gizmos.DrawSphere(_startDrag, 0.1f);

        Gizmos.DrawRay(_camPoint);
    }

}
