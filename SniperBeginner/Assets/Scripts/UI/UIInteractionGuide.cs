using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIInteractionGuide : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI guideText;


    private void Start() 
    {
        CharacterManager.Instance.Player.Interact.OnDetected += UpdateUI;

        UpdateUI(null);
    }

    void UpdateUI(InteractionData data)
    {
        gameObject.SetActive(data != null);

        if(data != null)
            guideText.text = $"키를 눌러 {data.ConvertTypeToString(data.type)}";
    }
}
