using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerView : MonoBehaviour
{
    [SerializeField] Transform cameraContainer;

    PlayerSetting setting;
    Vector2 delta = Vector2.zero;
    float currentRotateY = 0f;

    
    private void Start() 
    {
        if (TryGetComponent(out Player player))
        {
            setting = player.setting;
            
            // 생애주기를 함께할 것이라 구독 취소는 구현 안함
            player.Input.Actions.Look.performed += OnLook;
            player.Input.Actions.Look.canceled += OnLook;
        }

        currentRotateY = cameraContainer.transform.localEulerAngles.y;

        // 임시
        SetCursor(true);
    }

    private void LateUpdate() 
    {
        Look();
    }


    void OnLook(InputAction.CallbackContext context)
    {
        delta = context.ReadValue<Vector2>();
    }

    void Look()
    {
        Vector2 rotateSpeed = setting.lookSensitive * Time.deltaTime * delta;
        transform.Rotate(Vector3.up, rotateSpeed.x);

        currentRotateY -= rotateSpeed.y;
        currentRotateY = Mathf.Clamp(currentRotateY, setting.lookYAxisLimit.x, setting.lookYAxisLimit.y);
        cameraContainer.localEulerAngles = new Vector3(currentRotateY, 0f, 0f);
    }

    // 임시
    void SetCursor(bool isLocked)
    {
        Cursor.lockState = isLocked ? CursorLockMode.Locked : CursorLockMode.None;
    }
}
