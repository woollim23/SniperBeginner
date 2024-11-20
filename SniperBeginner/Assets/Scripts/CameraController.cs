using Cinemachine;
using System;
using System.Collections;
using UnityEngine;

using Random = UnityEngine.Random;

public class CinemachineProjectileSetting
{
    public Transform projectile;
    public Transform destination;
    public Vector3 startPosition;

    public Action onEnd;
}

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera mainCamera;
    public CinemachineVirtualCamera aimCamera;
    public CinemachineVirtualCamera bulletCamera;
    [SerializeField] [Range(0.5f, 3f)] float cameraRandomOffset;

    private float defaultTimeScale = 1f;
    private float slowMotionScale = 0.2f;

    private void Start()
    {
        SubscribeBulletEvents();
        SwitchToIdle();
    }

    private void SubscribeBulletEvents()
    {
        CharacterManager.Instance.Player.Shooting.OnSnipeEnemy += SwitchToBullet;
    }


    public void SwitchToIdle()
    {
        mainCamera.Priority = 10;
        aimCamera.Priority = 5;
        bulletCamera.Priority = 0;

        ResetTimeScale();
    }


    public void SwitchToBullet(CinemachineProjectileSetting setting)
    {
        var orbit = bulletCamera.GetCinemachineComponent<CinemachineOrbitalTransposer>();

        if(orbit != null)
        {
            Vector3 random = Random.onUnitSphere * Random.Range(0.5f, cameraRandomOffset);
            orbit.m_FollowOffset = random;
        }

        bulletCamera.Priority = 15;
        bulletCamera.Follow = setting.projectile;
        bulletCamera.LookAt =  setting.projectile;//setting.destination;

        ApplySlowMotion();

        StartCoroutine(HandleBulletCamera(setting));
    }

    private void ApplySlowMotion()
    {
        Time.timeScale = slowMotionScale;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }

    private void ResetTimeScale()
    {
        Time.timeScale = defaultTimeScale;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }


    private IEnumerator HandleBulletCamera(CinemachineProjectileSetting setting)
    {
        yield return StartCoroutine(MoveBullet(setting.projectile, setting.startPosition, setting.destination.position));
        setting.onEnd?.Invoke();

        ResetTimeScale(); 
        SwitchToIdle();
    }

    private IEnumerator MoveBullet(Transform bullet, Vector3 firePoint, Vector3 targetPosition)
    {
        float distance = Vector3.Distance(firePoint, targetPosition); 
        float travelTime = distance / 50f;
        float elapsedTime = 0f;

        bullet.position = firePoint;

        while (elapsedTime < travelTime)
        {
            elapsedTime += Time.deltaTime;
            bullet.position = Vector3.Lerp(firePoint, targetPosition, elapsedTime / travelTime); 
            yield return null;
        }

        bullet.position = targetPosition;
    }

}
