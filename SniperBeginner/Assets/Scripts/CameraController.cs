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
    [SerializeField][Range(0.3f, 1f)] float cameraMinOffset;
    [SerializeField][Range(1f, 10f)] float cameraMaxOffset;
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

        Vector3 sphericalPos = Random.onUnitSphere * Random.Range(cameraMinOffset, cameraMaxOffset);
        Vector3 newPos = sphericalPos + startPosition;
        Vector3 dir = (newPos - startPosition).normalized;
        float dot = Vector3.Dot(dir, projectile.forward);

        if(dot < 0f)
        {
            sphericalPos *= -1;
        }

        bulletCamera.transform.position = startPosition + sphericalPos;

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
        yield return StartCoroutine(CharacterManager.Instance.Player.Shooting.MoveBullet(projectile, startPosition, destination, travelSpeed));
        onEnd?.Invoke();

        ResetTimeScale(); 
        SwitchToIdle();
    }

}
