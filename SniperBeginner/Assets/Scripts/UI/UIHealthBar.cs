using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Slider))]
public class UIHealthBar : MonoBehaviour
{
    Slider slider;
    [SerializeField] TextMeshProUGUI healthLabel;

    private void Awake() 
    {
        slider = GetComponent<Slider>();
    }

    private void Start() 
    {
        CharacterManager.Instance.Player.Condition.OnHealthChanged += UpdateUI;

        UpdateUI(CharacterManager.Instance.Player.Condition.GetPercent());
    }

    void UpdateUI(float percent)
    {
        slider.value = percent;
        if(healthLabel)
            healthLabel.text = string.Format("{0:N2} %", percent * 100f);

    }
}
