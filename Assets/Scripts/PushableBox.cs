using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableBox : MonoBehaviour
{
    Vector3 pushDir;
    bool collided = false;
    GameObject m_player;

    public void Awake()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Update()
    {
        if(m_player == null) m_player = GameObject.FindGameObjectWithTag("Player");

        Move();
    }

    public void Move()
    {
        if (!collided)
            return;

        transform.Translate(pushDir * 2f);

        collided = false;
    }

    public void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject == m_player)
        {
            collided = true;
            if(m_player.transform.position.x > transform.position.x)
            {
                pushDir = new Vector3(-1, 0, 0);
            }else if (m_player.transform.position.x < transform.position.x)
            {
                pushDir = new Vector3(1, 0, 0);
            }else if (m_player.transform.position.z > transform.position.z)
            {
                pushDir = new Vector3(0, 0, -1);
            }else if (m_player.transform.position.z < transform.position.z)
            {
                pushDir = new Vector3(0, 0, 1);
            }
        }
    }
}
