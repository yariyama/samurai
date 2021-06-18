using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car1Manager : MonoBehaviour
{
    //座標
    private Vector2 _position;
    //スケール
    private Vector2 _scale;


    //ステータス
    private int _st = 1;
    //スピード
    public float _speed = 1;
    //タイマー
    private float _timer;

    //_st=1-基本形
    //_st=2-移動

    void Awake()
    {
        _position = transform.position;
        _scale = transform.localScale;
    }

    void Start()
    {
        _st = 1;
        _speed = 1;
        _timer = 0;
    }

    void FixedUpdate()
    {
        if (_st == 2)
        {
            _position.x += _speed / 50;

            transform.position = _position;

            _timer += Time.deltaTime;
            if (_timer >= 2)
            {
                _timer = 0;
                _speed *= -1;
                _scale.x *= -1;
            }

            transform.localScale = _scale;
        }
    }

    private void OnMouseDown()
    {
        if (_st == 1)
        {
            _st = 2;
        }
        else
        {
            _st = 1;
        }
    }
}
