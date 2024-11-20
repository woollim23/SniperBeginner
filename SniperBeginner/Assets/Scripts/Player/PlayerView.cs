using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerView : MonoBehaviour
{
    PlayerSetting setting;
    PlayerShootingController shooting;

    Vector2 delta = Vector2.zero;
    float currentRotateY = 0f;

    [SerializeField] Transform cameraContainer;
    public CinemachineVirtualCamera mainVirtualCam;
    public CinemachineVirtualCamera aimVirtualCam;

    Transform aimPoint;

    private void Awake() 
    {
        CameraManager.Instance.mainVirtualCamera = mainVirtualCam;
        CameraManager.Instance.aimVirtualCamera = aimVirtualCam;    
    }

    private void Start() 
    {
        if (TryGetComponent(out Player player))
        {
            setting = player.setting;
            shooting = player.Shooting;
            
            player.Actions.Look.performed += OnLook;
            player.Actions.Look.canceled += OnLook;
        }

        currentRotateY = cameraContainer.transform.localEulerAngles.y;
    }

    private void LateUpdate() 
    {
        Look();

        if (shooting.isAiming)
            aimVirtualCam.transform.position = aimPoint.position;
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
}
