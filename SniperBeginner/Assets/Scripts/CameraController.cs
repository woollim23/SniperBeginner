using Cinemachine;
using System;
using System.Collections;
using UnityEngine;

using Random = UnityEngine.Random;


public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera mainCamera;
    public CinemachineVirtualCamera aimCamera;
    public CinemachineVirtualCamera generalAimCamera;

    public CinemachineVirtualCamera bulletCamera;
    [SerializeField][Range(0.5f, 5f)] float cameraMinOffset;
    [SerializeField][Range(5f, 10f)] float cameraMaxOffset;
    [SerializeField] float travelSpeed = 25f;

    private float defaultTimeScale = 1f;
    private float slowMotionScale = 0.1f;
    float originFixedDeltaTime = 0f;

    private void Start()
    {
        originFixedDeltaTime = Time.fixedDeltaTime;

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
        generalAimCamera.Priority = 5;
        bulletCamera.Priority = 0;

        ResetTimeScale();
    }


    public void SwitchToBullet(Transform projectile, Transform destination, Vector3 startPosition, Action onEnd)
    {
        // Stand by에서 세팅
        bulletCamera.Follow = projectile;
        bulletCamera.LookAt = projectile; //setting.destination;

        bulletCamera.transform.position = startPosition + Random.onUnitSphere * Random.Range(cameraMinOffset, cameraMaxOffset);

        var transposer = bulletCamera.GetCinemachineComponent<CinemachineTransposer>();
        if(transposer != null)
        {
            Vector3 random = Random.onUnitSphere * Random.Range(cameraMinOffset, cameraMaxOffset);
            random.z = -cameraMinOffset;
            transposer.m_FollowOffset = random;
        }
        
        // Live 전환
        bulletCamera.Priority = 15;

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
        Time.fixedDeltaTime = originFixedDeltaTime;
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
        float sqrDistance = Vector3.SqrMagnitude(firePoint - destination.position);
        float travelTime = sqrDistance / (travelSpeed * travelSpeed);
        float elapsedTime = 0f;

        Vector3 end = destination.position;
        Debug.Log(destination.name);

        while (elapsedTime < travelTime)
        {
            elapsedTime += Time.deltaTime;
            bullet.position = Vector3.Lerp(firePoint, end, elapsedTime / travelTime);  //
            
            yield return null;
        }

        bullet.position = destination.position;
    }

}
