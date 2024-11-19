using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBreathBar : MonoBehaviour
{
    [SerializeField] Slider bar;

    private void Awake() {
        bar = GetComponent<Slider>();
    }

    private void Start() 
    {
        CharacterManager.Instance.Player.Shooting.OnControlBreath += UpdateUI;
    }

    void UpdateUI(float percent)
    {
        bar.value = percent;
    }
}
