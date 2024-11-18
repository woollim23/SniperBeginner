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
        SwitchToIdle(); // 초기 카메라 상태
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
        // CharacterManager.Instance.Player.Shooting.OnKilledEnemy += SwitchToBullet;
    }

    private void UnsubscribeBulletEvents()
    {
        // CharacterManager.Instance.Player.Shooting.OnKilledEnemy -= SwitchToBullet;
    }


    public void SwitchToIdle()
    {
        MainCamera.Priority = 10;  // 기본 카메라 우선 순위 상승
        aimCamera.Priority = 5;   // 에임 카메라 비활성화
        bulletCamera.Priority = 0; // 총알 카메라 비활성화

        ResetTimeScale(); // 원래 속도로 복구
    }

    public void SwitchToAim()
    {
        MainCamera.Priority = 5;
        aimCamera.Priority = 10;  // 에임 카메라 우선 순위 상승
        bulletCamera.Priority = 0;

        ResetTimeScale(); // 혹시 남아있을 슬로우 모션 복구
    }

    public void SwitchToBullet(Transform bullet, Transform target)
    {
        bulletCamera.Priority = 15; // 총알 카메라 활성화
        bulletCamera.Follow = bullet;
        bulletCamera.LookAt = target;

        ApplySlowMotion(); // 슬로우 모션 활성화

        StartCoroutine(ResetToIdle(1f)); // 1초 뒤 Idle로 복귀
    }

    private void ApplySlowMotion()
    {
        Time.timeScale = slowMotionScale; // 슬로우 모션 활성화
        Time.fixedDeltaTime = Time.timeScale * 0.02f; // 물리 계산도 슬로우에 맞게 조정
    }

    private void ResetTimeScale()
    {
        Time.timeScale = defaultTimeScale; // 원래 속도로 복구
        Time.fixedDeltaTime = Time.timeScale * 0.02f; // 물리 계산 복구
    }

    private IEnumerator ResetToIdle(float delay)
    {
        yield return new WaitForSeconds(delay); // 현실 시간 기준 대기
        ResetTimeScale(); // 시간 복구
        SwitchToIdle();
    }

   
}
