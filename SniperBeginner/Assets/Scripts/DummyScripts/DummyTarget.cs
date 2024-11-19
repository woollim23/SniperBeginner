using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyTarget : MonoBehaviour, ISnipable
{
    public float CheckRemainHealth()
    {
        return 1f;
    }
}
