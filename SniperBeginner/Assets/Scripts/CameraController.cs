using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineBullet
{
    public Transform projectile;
    public Transform destination;
    public Vector3 startPosition;

    public Action OnHit;
}

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera MainCamera;
    public CinemachineVirtualCamera aimCamera;
    public CinemachineVirtualCamera bulletCamera;

    private float defaultTimeScale = 1f;
    private float slowMotionScale = 0.2f;

    private void Start()
    {
        SwitchToIdle();
    }


    private void OnEnable()
    { 
        SubscribeBulletEvents();
    }

    private void OnDisable()
    {
        UnsubscribeBulletEvents(); 
    }

    private void SubscribeBulletEvents()
    {
        CharacterManager.Instance.Player.Shooting.OnSnipeEnemy += SwitchToBullet;
    }

    private void UnsubscribeBulletEvents()
    {
        CharacterManager.Instance.Player.Shooting.OnSnipeEnemy -= SwitchToBullet;
    }


    public void SwitchToIdle()
    {
        MainCamera.Priority = 10;
        aimCamera.Priority = 5;
        bulletCamera.Priority = 0;

        ResetTimeScale();
    }

    public void SwitchToBullet(Transform bullet, Vector3 firePoint, Transform target)
    {
        bulletCamera.Priority = 15;
        bulletCamera.Follow = bullet;
        bulletCamera.LookAt = target;

        ApplySlowMotion();

        StartCoroutine(HandleBulletCamera(bullet, firePoint, target.position));
    }

    public void SwitchToBullet(CinemachineBullet setting)
    {
        bulletCamera.Priority = 15;
        bulletCamera.Follow = setting.projectile;
        bulletCamera.LookAt = setting.destination;

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

    private IEnumerator HandleBulletCamera(Transform bullet, Vector3 firePoint, Vector3 targetPosition)
    {
        yield return StartCoroutine(MoveBullet(bullet, firePoint, targetPosition));

        ResetTimeScale(); 
        SwitchToIdle();
    }

    private IEnumerator HandleBulletCamera(CinemachineBullet setting)
    {
        yield return StartCoroutine(MoveBullet(setting.projectile, setting.startPosition, setting.destination.position));
        setting.OnHit?.Invoke();

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
