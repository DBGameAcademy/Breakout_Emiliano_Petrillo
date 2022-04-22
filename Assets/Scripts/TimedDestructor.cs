using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDestructor : MonoBehaviour
{
    public float WaitTime = 1.5f;

    private void Start()
    {
        Destroy(gameObject, WaitTime);
    }
}
