using Cinemachine;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    public CameraController cameraController;
    
    [Space(10f)]
    public CinemachineVirtualCamera mainVirtualCamera;
    public CinemachineVirtualCamera aimVirtualCamera;
    public CinemachineVirtualCamera bulletVirtualCamera;


    public void Initialize()
    {
        cameraController.mainCamera = mainVirtualCamera;
        cameraController.aimCamera = aimVirtualCamera;
        cameraController.bulletCamera = bulletVirtualCamera;
    }
}