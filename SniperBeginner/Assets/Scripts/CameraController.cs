using Cinemachine;
using System;
using System.Collections;
using UnityEngine;

using Random = UnityEngine.Random;


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
        CharacterManager.Instance.Player.Shooting.OnSnipe += SwitchToBullet;
    }


    public void SwitchToIdle()
    {
        mainCamera.Priority = 10;
        aimCamera.Priority = 5;
        bulletCamera.Priority = 0;

        ResetTimeScale();
    }


    public void SwitchToBullet(Transform projectile, Transform destination, Vector3 startPosition, Action onEnd)
    {
        var orbit = bulletCamera.GetCinemachineComponent<CinemachineOrbitalTransposer>();

        if(orbit != null)
        {
            Vector3 random = Random.onUnitSphere * Random.Range(0.5f, cameraRandomOffset);
            orbit.m_FollowOffset = random;
        }

        bulletCamera.Priority = 15;
        bulletCamera.Follow = projectile;
        bulletCamera.LookAt = projectile;//setting.destination;

        ApplySlowMotion();

        StartCoroutine(HandleBulletCamera(projectile, destination, startPosition, onEnd));
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


    private IEnumerator HandleBulletCamera(Transform projectile, Transform destination, Vector3 startPosition, Action onEnd)
    {
        yield return StartCoroutine(MoveBullet(projectile, startPosition, destination));
        onEnd?.Invoke();

        ResetTimeScale(); 
        SwitchToIdle();
    }

    private IEnumerator MoveBullet(Transform bullet, Vector3 firePoint, Transform destination)
    {
        float distance = Vector3.Distance(firePoint, destination.position); 
        float travelTime = distance / 50f;
        float elapsedTime = 0f;

        bullet.position = firePoint;

        while (elapsedTime < travelTime)
        {
            elapsedTime += Time.deltaTime;
            bullet.position = Vector3.Lerp(firePoint, destination.position, elapsedTime / travelTime); 
            yield return null;
        }

        bullet.position = destination.position;
    }

}
