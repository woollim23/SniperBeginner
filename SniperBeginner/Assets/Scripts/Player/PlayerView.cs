using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerView : MonoBehaviour
{
    PlayerSetting setting;
    PlayerShootingController shooting;

    Vector2 delta = Vector2.zero;
    float currentRotateY = 0f;

    [SerializeField] Transform cameraContainer;
    [SerializeField] Transform aimCamera;
    Transform aimPoint;


    private void Start() 
    {
        if (TryGetComponent(out Player player))
        {
            setting = player.setting;
            shooting = player.Shooting;
            
            // 생애주기를 함께할 것이라 구독 취소는 구현 안함
            player.Actions.Look.performed += OnLook;
            player.Actions.Look.canceled += OnLook;
        }

        currentRotateY = cameraContainer.transform.localEulerAngles.y;

        // 임시
        SetCursor(true);
    }

    private void LateUpdate() 
    {
        Look();

        if (shooting.isAiming)
            aimCamera.position = aimPoint.position;
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

    public void UpdateAimPosition(Transform newAimPoint)
    {
        aimPoint = newAimPoint ? newAimPoint : null;
    }


    // 임시
    void SetCursor(bool isLocked)
    {
        Cursor.lockState = isLocked ? CursorLockMode.Locked : CursorLockMode.None;
    }
}
