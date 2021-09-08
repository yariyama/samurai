using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boy2Manager : MonoBehaviour
{
    //座標
    private Vector2 _position;

    //ステータス
    private int _st;
    //カウント
    private int _count;
    //ジャンプスピード
    private float _j_speed;
    //ジャンプスピードベース
    public float _j_speed_b;
    //重力
    public float _g;

    //_st=1-基本形
    //_st=2-ジャンプ

    void Awake()
    {
        _position = transform.position;
    }

    void Start()
    {
        _st = 1;
        _count = 0;
        _j_speed = _j_speed_b;
        _position.y = 0;
        transform.position = _position;
    }

    void FixedUpdate()
    {
        if (_st==2)
        {
            
            if (_count==0)
            {
                //上昇
                _position.y += _j_speed;
                _j_speed -= _g;
                if (_j_speed<=0)
                {
                    _j_speed = 0;
                    _count = 1;
                }
            }
            else if (_count==1)
            {
                //下降
                _position.y -= _j_speed;
                _j_speed += _g;
                if (_position.y <= 0)
                {
                    _position.y = 0;
                    Start();
                }
            }

            transform.position = _position;

        }
    }

    void OnMouseDown()
    {
        if (_st==1)
        {
            _st = 2;
        }
    }
}
