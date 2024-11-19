using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyInteract : ItemInteractable
{
    [SerializeField] InteractionData interact;
    [SerializeField] GeneratedItem gi;
    private void Start() 
    {
        interact.interactKey = KeyCode.F;
        Initialize(gi);
    }
}
