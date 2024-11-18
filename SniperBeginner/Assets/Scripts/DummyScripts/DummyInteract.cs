using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyInteract : MonoBehaviour, IInteractable
{
    [SerializeField] InteractionData interact;
    private void Start() 
    {
        interact = new InteractionData(InterationType.Pick);    
    }

    public InteractionData GetInformation()
    {
        return interact;
    }

    public void Interact()
    {
        Debug.Log("Interact");
        Destroy(gameObject);
    }
}
