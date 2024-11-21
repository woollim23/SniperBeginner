using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyStarter : MonoBehaviour
{
    [SerializeField] float offset = 1f;
    [SerializeField] Transform firePoint;
    [SerializeField] Transform bullet;
    [SerializeField] Transform camera;


    [ContextMenu("Test")]
    public void TestDebug()
    {
        Vector3 sphericalPos = Random.onUnitSphere * offset;
        camera.position = sphericalPos;

        Vector3 dir = (camera.position - firePoint.position).normalized;

        float dot = Vector3.Dot(dir, firePoint.forward);
        if(dot < 0f)
        {
            sphericalPos = -sphericalPos;
            camera.position = sphericalPos;

            Debug.Log("Inverted");
        }
        
        Debug.Log(dir);
        Debug.Log("Dot : " + Vector3.Dot(dir, firePoint.forward));
    }

    
    [ContextMenu("Get Dot")]
    public void GetDot()
    {
        Vector3 dir = (camera.position - firePoint.position).normalized;
        Debug.Log("Dot : " + Vector3.Dot(dir, firePoint.forward));
    }
}
