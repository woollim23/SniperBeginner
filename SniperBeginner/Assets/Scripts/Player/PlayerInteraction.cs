using System;
using UnityEngine;


public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] LayerMask layerMask = 1 << 3;
    IInteractable interactable;

    public event Action<InteractionData> OnDetected;

    private void Start() 
    {
        Debug.Log("PlayerInteraction start");

        if(TryGetComponent(out PlayerInputController input))
        {
            Debug.Log(input.Actions.Interaction == null);
            input.Actions.Interaction.started += (context) => { Interact(); };
        }
    }

    public void DetectObject(IInteractable thing)
    {
        interactable = thing;
        OnDetected?.Invoke(interactable.GetInformation());
    }

    public void ClearObject()
    {
        interactable = null;
        OnDetected?.Invoke(null);
    }


    public void Interact() 
    {
        interactable?.Interact();

        ClearObject();
    }
}