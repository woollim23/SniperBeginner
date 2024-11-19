using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera MainCamera;
    public CinemachineVirtualCamera aimCamera;
    public CinemachineVirtualCamera bulletCamera;

    private float defaultTimeScale = 1f;
    private float slowMotionScale = 0.2f;

    private void Start()
    {
        SubscribeBulletEvents();
        SwitchToIdle(); // 초기 카메라 상태
    }


    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        UnsubscribeBulletEvents();
    }

    private void SubscribeBulletEvents()
    {
        CharacterManager.Instance.Player.Shooting.OnKilledEnemy += SwitchToBullet;
    }

    private void UnsubscribeBulletEvents()
    {
        CharacterManager.Instance.Player.Shooting.OnKilledEnemy -= SwitchToBullet;
    }


    public void SwitchToIdle()
    {
        MainCamera.Priority = 10;  // 기본 카메라 우선 순위 상승
        aimCamera.Priority = 5;   // 에임 카메라 비활성화
        bulletCamera.Priority = 0; // 총알 카메라 비활성화

        ResetTimeScale(); // 원래 속도로 복구
    }

    public void SwitchToBullet(Transform bullet, Vector3 firePoint, Transform target)
    {
        bulletCamera.Priority = 15; // 총알 카메라 활성화
        bulletCamera.Follow = bullet;
        bulletCamera.LookAt = target;

        ApplySlowMotion(); // 슬로우 모션 활성화

        StartCoroutine(HandleBulletCamera(bullet, firePoint, target.position));
    }

    private void ApplySlowMotion()
    {
        Time.timeScale = slowMotionScale; // 슬로우 모션 활성화
        Time.fixedDeltaTime = Time.timeScale * 0.02f; // 물리 계산도 슬로우에 맞게 조정
    }

    private void ResetTimeScale()
    {
        Time.timeScale = defaultTimeScale; // 기본 속도로 복구
        Time.fixedDeltaTime = Time.timeScale * 0.02f; // 물리 계산 복구
    }

    private IEnumerator HandleBulletCamera(Transform bullet, Vector3 firePoint, Vector3 targetPosition)
    {
        // 총알 이동
        yield return StartCoroutine(MoveBullet(bullet, firePoint, targetPosition));

        // 총알 이동 후 상태 복구
        ResetTimeScale(); // 기본 속도로 복구
        SwitchToIdle();   // 기본 카메라로 복귀
    }

    private IEnumerator MoveBullet(Transform bullet, Vector3 firePoint, Vector3 targetPosition)
    {
        float distance = Vector3.Distance(firePoint, targetPosition); // 발사 지점과 타겟 위치 사이의 거리
        float travelTime = distance / 50f; // 이동 시간 = 거리 / 총알 속도 (TODO:: Weapon.cs에서 가져오기)
        float elapsedTime = 0f; // 경과 시간

        bullet.position = firePoint; // 총알을 발사 지점에 배치

        while (elapsedTime < travelTime)
        {
            elapsedTime += Time.deltaTime;
            bullet.position = Vector3.Lerp(firePoint, targetPosition, elapsedTime / travelTime); // 총알을 이동
            yield return null;
        }

        bullet.position = targetPosition; // 최종 위치 보정
    }

}
