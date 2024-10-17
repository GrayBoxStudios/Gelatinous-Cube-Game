using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelResetSystem : MonoBehaviour
{
    public GameObject originalLevel;
    public GameObject currentLevel;

    Controls m_controls;
    public void Start()
    {
        m_controls = new Controls();
        m_controls.Enable();
    }

    public void Awake()
    {
        if(currentLevel == null)
        currentLevel = Instantiate(originalLevel);
    }

    void Update()
    {
        if (m_controls.LevelReset.ResetLevel.triggered)
        {
            Destroy(currentLevel.gameObject);
            currentLevel = Instantiate(originalLevel);
        }
    }
}
