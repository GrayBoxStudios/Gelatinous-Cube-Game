using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlateData : MonoBehaviour
{
    public UnityEvent EnterEvent;
    public UnityEvent ExitEvent;

    public LayerMask HeavyObjects;

    public enum Types {Light, Heavy};
    public Types Type;

    int contacts = 0;

    void OnTriggerEnter(Collider collider)
    {
        if(Type == Types.Heavy)
        {
            if(!IsInLayerMask(collider.gameObject, HeavyObjects))
                return;
        }

        EnterEvent.Invoke();
        contacts++;
    }
    void OnTriggerExit(Collider collider)
    {
        if (Type == Types.Heavy)
        {
            if (!IsInLayerMask(collider.gameObject, HeavyObjects))
                return;
        }

        contacts--;

        if(contacts == 0)
            ExitEvent.Invoke();
    }

    bool IsInLayerMask(GameObject obj, LayerMask mask) => (mask.value & (1 << obj.layer)) != 0;
}
