using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SlimePathData : MonoBehaviour
{
    public bool alive = true;
    public int index = 6;
    public int max = 6;
    public TextMeshProUGUI text;
    public GameObject slimeVisual;
    public float scaler = 0f;

    public void Update()
    {
        text.text = index.ToString();
        scaler = (max - index + 1f) / max;
        scaler = Mathf.Sqrt(scaler);
        slimeVisual.transform.localScale = Vector3.one * 600f * scaler;

        if (!alive) transform.Translate(Vector3.down * Time.deltaTime);
    }

    public void Kill()
    {
        alive = false;
        Destroy(gameObject, 0.5f);
    }

}
