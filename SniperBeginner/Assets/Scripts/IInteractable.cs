using UnityEngine;

public interface IInteractable
{   
    InteractionData GetInformation();
    void Interact();
}


[System.Serializable]
public class InteractionData
{
    public KeyCode interactKey;
    public InterationType type;
    public string interactionName;
    public string description;

    public InteractionData(InterationType initType)
    {
        interactKey = KeyCode.F;
        type = initType;
    }

    public string ConvertTypeToString(InterationType type)
    {
        switch (type)
        {
            case InterationType.Pick:
                return "줍기";
            case InterationType.Open:
                return "열기";
            default :
                return "상호작용하기";
        }
    }
}