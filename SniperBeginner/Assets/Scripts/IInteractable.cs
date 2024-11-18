using UnityEngine;

public interface IInteractable
{   
    InteractionData GetInformation();
    void Interact();
}

public enum InterationType
{
    Pick, // 줍기
    Open, // 열기
}

[System.Serializable]
public class InteractionData
{
    public KeyCode interactKey;
    public InterationType type;

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