using System;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] LayerMask interactLayerMask = 1 << 3 | 1 << 8;

    List<IInteractable> interactables = new List<IInteractable>();

    public event Action<InteractionData> OnDetected;

    private void Start() 
    {
        if(TryGetComponent(out Player player))
        {
            player.Actions.Interaction.started += (context) => { Interact(); };
        }
    }

    public void DetectObject(IInteractable thing)
    {
        interactables.Add(thing);
        
        IInteractable i = GetNextInteractable();

        if(i != null)
            OnDetected?.Invoke(i.GetInformation());
        else
            OnDetected?.Invoke(null);
    }

    public void ClearObject(IInteractable thing)
    {
        if(interactables.Contains(thing))
            interactables.Remove(thing);


        IInteractable i = GetNextInteractable();

        if(i != null) // 있으면 다음 거 보여주고
            OnDetected?.Invoke(i.GetInformation());
        else // 없으면 정리
            OnDetected?.Invoke(null);
    }

    public void Interact() 
    {
        IInteractable interactable = GetNextInteractable();

        if (interactable != null)
        {
            interactable?.Interact();
            ClearObject(interactable);
        }
    }

    IInteractable GetNextInteractable()
    {
        for (int i = 0; i < interactables.Count; i++)
        {
            int id = interactables.Count - 1 - i;

            if (interactables[id] != null)
                return interactables[id];
            else
            {
                interactables.RemoveAt(id);
                i--;
            }
        }

        return null;
    }
}