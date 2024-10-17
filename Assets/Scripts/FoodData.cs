using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodData : MonoBehaviour
{
    public int slotsRestored = 6;

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>())
        {
            PlayerMovement.MoveSlots = slotsRestored;
            Destroy(gameObject);
        }
    }

}
