using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationManager : MonoBehaviour
{
    //ステータス
    private int _st;
    //回転量
    public float _angle;

    //_st=1-基本形
    //_st=2-回転

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _angle = 90;
    }

    void FixedUpdate()
    {
        if (_st==2) {
            transform.Rotate(0, 0, _angle / 50);
        }
    }

    public void RoteSet()
    {
        if (_st==1)
        {
            _st = 2;
        }
        else
        {
            _st = 1;
        }
    }
}
