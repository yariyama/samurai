using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car1Manager : MonoBehaviour
{
    private float _speed;

    void FixedUpdate()
    {
        transform.Translate(_speed / 50, 0, 0);
    }

    void OnMouseDown()
    {
        _speed = 5;
    }

    void OnMouseUp()
    {
        _speed = 0;
    }
}
