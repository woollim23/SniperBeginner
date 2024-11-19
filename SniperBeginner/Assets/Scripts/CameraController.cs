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
        SwitchToIdle(); // �ʱ� ī�޶� ����
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
        MainCamera.Priority = 10;  // �⺻ ī�޶� �켱 ���� ���
        aimCamera.Priority = 5;   // ���� ī�޶� ��Ȱ��ȭ
        bulletCamera.Priority = 0; // �Ѿ� ī�޶� ��Ȱ��ȭ

        ResetTimeScale(); // ���� �ӵ��� ����
    }

    public void SwitchToBullet(Transform bullet, Vector3 firePoint, Transform target)
    {
        bulletCamera.Priority = 15; // �Ѿ� ī�޶� Ȱ��ȭ
        bulletCamera.Follow = bullet;
        bulletCamera.LookAt = target;

        ApplySlowMotion(); // ���ο� ��� Ȱ��ȭ

        StartCoroutine(HandleBulletCamera(bullet, firePoint, target.position));
    }

    private void ApplySlowMotion()
    {
        Time.timeScale = slowMotionScale; // ���ο� ��� Ȱ��ȭ
        Time.fixedDeltaTime = Time.timeScale * 0.02f; // ���� ��굵 ���ο쿡 �°� ����
    }

    private void ResetTimeScale()
    {
        Time.timeScale = defaultTimeScale; // �⺻ �ӵ��� ����
        Time.fixedDeltaTime = Time.timeScale * 0.02f; // ���� ��� ����
    }

    private IEnumerator HandleBulletCamera(Transform bullet, Vector3 firePoint, Vector3 targetPosition)
    {
        // �Ѿ� �̵�
        yield return StartCoroutine(MoveBullet(bullet, firePoint, targetPosition));

        // �Ѿ� �̵� �� ���� ����
        ResetTimeScale(); // �⺻ �ӵ��� ����
        SwitchToIdle();   // �⺻ ī�޶�� ����
    }

    private IEnumerator MoveBullet(Transform bullet, Vector3 firePoint, Vector3 targetPosition)
    {
        float distance = Vector3.Distance(firePoint, targetPosition); // �߻� ������ Ÿ�� ��ġ ������ �Ÿ�
        float travelTime = distance / 50f; // �̵� �ð� = �Ÿ� / �Ѿ� �ӵ� (TODO:: Weapon.cs���� ��������)
        float elapsedTime = 0f; // ��� �ð�

        bullet.position = firePoint; // �Ѿ��� �߻� ������ ��ġ

        while (elapsedTime < travelTime)
        {
            elapsedTime += Time.deltaTime;
            bullet.position = Vector3.Lerp(firePoint, targetPosition, elapsedTime / travelTime); // �Ѿ��� �̵�
            yield return null;
        }

        bullet.position = targetPosition; // ���� ��ġ ����
    }

}
