using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClearManager : MonoBehaviour
{
    //座標
    private Vector2 _position;
    //角度
    private Vector3 _angle;

    //ステータス
    private int _st;

    //_st=1-基本形
    //_st=2-スライド
    //_st=3-回転

    void Awake()
    {
        _position = transform.localPosition;
        _angle = transform.localEulerAngles;
    }

    void FixedUpdate()
    {
        if (_st==2)
        {
            _position.x -= 20;
            if (_position.x<=0)
            {
                _position.x = 0;
                _st = 1;
            }
            transform.localPosition = _position;
        }
        else if (_st==3)
        {
            _angle.z += 5;
            if (_angle.z>=90)
            {
                _angle.z = 90;
                _st = 1;
            }
            transform.localEulerAngles = _angle;
        }
    }

    public void ActiveSet()
    {
        _st = 2;
        _position.x = 800;
        transform.localPosition = _position;
        //_st = 3;
    }
}
