using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangerData : MonoBehaviour
{
    public Transform StartPoint;
    public Transform EndPoint;

    public List<GameObject> AttachedObjects = new List<GameObject>();

    GameObject m_StartPoint;
    GameObject m_EndPoint;
    Vector3 prevPos;

    bool activated = false;
    float timer;

    public void Awake()
    {
        StartPoint = transform;

        m_StartPoint = new GameObject();
        m_EndPoint = new GameObject();

        m_StartPoint.transform.SetParent(transform.parent);
        m_EndPoint.transform.SetParent(transform.parent);

        m_StartPoint.transform.position = StartPoint.position;
        m_EndPoint.transform.position = EndPoint.position;
    }

    public void Update()
    {
        transform.position = Vector3.Lerp(m_StartPoint.transform.position, m_EndPoint.transform.position, timer);
        MoveAttached();

        if (activated)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer -= Time.deltaTime;
        }

        timer = Mathf.Clamp(timer, 0f, 1f);
        prevPos = transform.position;
    }

    void MoveAttached()
    {
        //Prune for null references
        for (int i = AttachedObjects.Count-1; i > -1; i--)
        {
            if(AttachedObjects[i] == null)
                AttachedObjects.RemoveAt(i);
        }

        if (transform.position != prevPos)
        {
            PlayerMovement.FreezeControl = true;
        }
        for(int i = 0; i < AttachedObjects.Count; i++)
        {
            AttachedObjects[i].transform.Translate(transform.position - prevPos);
        }
    }

    public void Activate()
    {
        activated = true;
    }
    public void Deactivate()
    {
        activated = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        AttachedObjects.Add(other.gameObject);
    }
    public void OnTriggerExit(Collider other)
    {
        AttachedObjects.Remove(other.gameObject);
    }
}
