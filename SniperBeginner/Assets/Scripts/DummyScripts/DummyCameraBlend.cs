using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class DummyCameraBlend : MonoBehaviour
{
    [SerializeField] int curid = 0;
    [SerializeField] CinemachineBrain brain;
    [SerializeField] CinemachineVirtualCamera[] cams;


    [ContextMenu("Test Change")]
    public void Test()
    {
        curid++;
        if(curid >= cams.Length)
            curid = 0;

        Change(curid);
    }


    void Change(int id)
    {
        for (int i = 0; i < cams.Length; i++)
        {
            cams[i].enabled = i == id;
            
        }
    }
}
