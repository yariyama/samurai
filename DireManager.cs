using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DireManager : MonoBehaviour
{
    //角度
    private Vector3 _angle;

    //ステータス
    private int _st;
    //方向
    private int _dire;
    //ターゲット角度
    private float _t_angle;


    //_st=1-基本形
    //_st=2-回転

    void Awake()
    {
        _angle = transform.localEulerAngles;
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _dire = 1;
    }

    void FixedUpdate()
    {
        if (_st==2)
        {
            _angle.z -= 2f;
            if (_angle.z<=_t_angle)
            {
                _angle.z = _t_angle;
                ++_dire;
                if (_dire==5)
                {
                    _dire = 1;
                    _angle.z = 0;
                }
                _st = 1;
            }
            transform.localEulerAngles=_angle;
        }
    }

    public void RoteSet()
    {
        _st = 2;
        if (_dire==1)
        {
            _t_angle = -90;
        }
        else if (_dire==2)
        {
            _t_angle = -180;
        }
        else if (_dire == 3)
        {
            _t_angle = -270;
        }
        else if (_dire == 4)
        {
            _t_angle = -360;
        }
    }
}
