using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioInstancer : MonoBehaviour
{
    public GameObject[] AudioToInstances;
    public void CreateAudio()
    {
        int randIndex = Random.Range(0, AudioToInstances.Length);

        GameObject instance = Instantiate(AudioToInstances[randIndex], transform);
        Destroy(instance, instance.GetComponent<AudioSource>().clip.length + 1f);
    }
}
