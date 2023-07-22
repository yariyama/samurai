using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubCameraManager : MonoBehaviour
{
    //角度
    private Vector3 _angle;

    //マウス座標
    private Vector3 _mouse;
    private Vector3 _target;

    void Awake()
    {
        _angle = transform.localEulerAngles;
    }

    // Start is called before the first frame update
    void Start()
    {
        _angle.x = 0;
        transform.localEulerAngles = _angle;
    }

    // Update is called once per frame
    void Update()
    {
        _mouse = Input.mousePosition;
        _mouse.z = 10;
        _target = Camera.main.ScreenToWorldPoint(_mouse);
    }

    void FixedUpdate()
    {
        if (_target.y>=2.5f)
        {
            if (_angle.x>=-30) {
                _angle.x -= 1;
                transform.localEulerAngles = _angle;
            }
        }
        else if (_target.y <= -0.5f)
        {
            if (_angle.x <= 30)
            {
                _angle.x += 1;
                transform.localEulerAngles = _angle;
            }
        }
        else
        {
            if (_angle.x<0)
            {
                _angle.x += 1;
                if (_angle.x>=0)
                {
                    _angle.x = 0;
                }
                transform.localEulerAngles = _angle;
            }
            else if (_angle.x > 0)
            {
                _angle.x -= 1;
                if (_angle.x <= 0)
                {
                    _angle.x = 0;
                }
                transform.localEulerAngles = _angle;
            }
        }
    }
}
