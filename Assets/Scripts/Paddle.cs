using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float v;

    void Start()
    {
        
    }

    public void Move(Vector2 _direction)
    {
        transform.Translate(_direction * v * Time.deltaTime);
    }
}
