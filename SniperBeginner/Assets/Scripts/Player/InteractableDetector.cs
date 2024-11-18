using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableDetector : MonoBehaviour
{
    public UnityEvent<IInteractable> OnDetect;
    public UnityEvent OnMiss;
    List<IInteractable> things = new List<IInteractable>();

    private void OnTriggerEnter(Collider other) 
    {
        if (other.TryGetComponent(out IInteractable thing))
        {
            if (!things.Contains(thing))
                things.Add(thing);

            OnDetect?.Invoke(thing);
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.TryGetComponent(out IInteractable thing))
        {
            if (things.Contains(thing))
                things.Remove(thing);

            OnMiss?.Invoke();
        }
    }
    
}