using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelComplete : MonoBehaviour
{
    GameObject m_player;
    bool collided = false;
    public UnityEvent onComplete;

    public void Awake()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Update()
    {
        if (m_player == null) m_player = GameObject.FindGameObjectWithTag("Player");

        if (collided)
        {
            onComplete.Invoke();
        }
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject == m_player)
        {
            collided = true;
        }
    }

}
