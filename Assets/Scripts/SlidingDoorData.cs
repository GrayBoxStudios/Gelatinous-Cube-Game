using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoorData : MonoBehaviour
{
    public Transform StartPoint;
    public Transform EndPoint;

    GameObject m_StartPoint;
    GameObject m_EndPoint;

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

        if (activated)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer -= Time.deltaTime;
        }

        timer = Mathf.Clamp(timer, 0f, 1f);
    }

    public void Activate()
    {
        activated = true;
    }
    public void Deactivate()
    {
        activated = false;
    }
}
