using UnityEngine;
using UnityEngine.Events;

public class InteractableDetector : MonoBehaviour
{
    public UnityEvent<IInteractable> OnDetect;
    public UnityEvent<IInteractable> OnMiss;

    private void OnTriggerEnter(Collider other) 
    {
        if (other.TryGetComponent(out IInteractable thing))
        {
            OnDetect?.Invoke(thing);
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.TryGetComponent(out IInteractable thing))
        {
            OnMiss?.Invoke(thing);
        }
    }
    
}