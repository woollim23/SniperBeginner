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
        SwitchToIdle(); // �ʱ� ī�޶� ����
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
        MainCamera.Priority = 10;  // �⺻ ī�޶� �켱 ���� ���
        aimCamera.Priority = 5;   // ���� ī�޶� ��Ȱ��ȭ
        bulletCamera.Priority = 0; // �Ѿ� ī�޶� ��Ȱ��ȭ

        ResetTimeScale(); // ���� �ӵ��� ����
    }

    public void SwitchToAim()
    {
        MainCamera.Priority = 5;
        aimCamera.Priority = 10;  // ���� ī�޶� �켱 ���� ���
        bulletCamera.Priority = 0;

        ResetTimeScale(); // Ȥ�� �������� ���ο� ��� ����
    }

    public void SwitchToBullet(Transform bullet, Transform target)
    {
        bulletCamera.Priority = 15; // �Ѿ� ī�޶� Ȱ��ȭ
        bulletCamera.Follow = bullet;
        bulletCamera.LookAt = target;

        ApplySlowMotion(); // ���ο� ��� Ȱ��ȭ

        StartCoroutine(ResetToIdle(1f)); // 1�� �� Idle�� ����
    }

    private void ApplySlowMotion()
    {
        Time.timeScale = slowMotionScale; // ���ο� ��� Ȱ��ȭ
        Time.fixedDeltaTime = Time.timeScale * 0.02f; // ���� ��굵 ���ο쿡 �°� ����
    }

    private void ResetTimeScale()
    {
        Time.timeScale = defaultTimeScale; // ���� �ӵ��� ����
        Time.fixedDeltaTime = Time.timeScale * 0.02f; // ���� ��� ����
    }

    private IEnumerator ResetToIdle(float delay)
    {
        yield return new WaitForSeconds(delay); // ���� �ð� ���� ���
        ResetTimeScale(); // �ð� ����
        SwitchToIdle();
    }

   
}
