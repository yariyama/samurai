using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boy1Manager : MonoBehaviour
{
    private Vector3 _angle;

    public float _speed = 1;

    private float _vx;
    private float _vz;

    void Awake()
    {
        _angle = transform.localEulerAngles;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _vx = 0;
        _vz = 0;

        if (Input.GetKey("right"))
        {
            _vx = _speed;
        }
        else if (Input.GetKey("left"))
        {
            _vx = -_speed;
        }

        if (Input.GetKey("up"))
        {
            _vz = _speed;
        }
        else if (Input.GetKey("down"))
        {
            _vz = -_speed;
        }
    }

    void FixedUpdate()
    {
        if (_vx!=0||_vz!=0)
        {
            if (_vz>0)
            {
                if (_vx>0)
                {
                    _angle.y = 45;
                }
                else if (_vx<0)
                {
                    _angle.y = 315;
                }
                else
                {
                    _angle.y = 0;
                }
            }
            else if (_vz < 0)
            {
                if (_vx > 0)
                {
                    _angle.y = 135;
                }
                else if (_vx < 0)
                {
                    _angle.y = 225;
                }
                else
                {
                    _angle.y = 180;
                }
            }
            else
            {
                if (_vx > 0)
                {
                    _angle.y = 90;
                }
                else if (_vx < 0)
                {
                    _angle.y = 270;
                }
            }
            transform.localEulerAngles=_angle;
            transform.Translate(_vx/50,0,_vz/50,Space.World);
        }
    }
}
