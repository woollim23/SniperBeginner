
using Cinemachine;
using UnityEngine;

public class DummyCameraBlend : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera[] cams;


    [ContextMenu("Test")]
    public void Test()
    {
        var a = cams[0].GetCinemachineComponent<CinemachineComposer>();
        Debug.Log(a != null);
        Debug.Log(a);

        a.m_TrackedObjectOffset = Vector3.one;
    }

}
