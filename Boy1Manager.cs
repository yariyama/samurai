using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boy1Manager : MonoBehaviour
{
    public float _speed = 1;

    private float _vx;
    private float _vz;

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
            transform.Translate(_vx/50,0,_vz/50);
        }
    }
}
