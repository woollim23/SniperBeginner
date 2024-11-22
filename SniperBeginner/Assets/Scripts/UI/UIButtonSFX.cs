using UnityEngine;
using UnityEngine.UI;

public class UIButtonSFX : MonoBehaviour
{
    [SerializeField] private Button btn;

    private void Awake()
    {
        if (btn == null)
        {
            btn = GetComponent<Button>();
        }

        btn.onClick.AddListener(() => SoundManager.Instance.PlaySound(SoundManager.Instance.buttonClickSFX, 0.7f));
    }
}