using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car3Manager : MonoBehaviour
{
    //座標
    private Vector2 _position;
    //スケール
    private Vector2 _scale;

    //ステータス
    private int _st;
    //横方向
    private int _dire_x;
    //縦方向
    private int _dire_y;
    //スピード
    public float _speed;

    //_st=1-基本形
    //_st=2-移動

    //_dire_x=1-右
    //_dire_x=2-左
    //_dire_y=1-上
    //_dire_y=2-下

    void Awake()
    {
        _position = transform.position;
        _scale = transform.localScale;
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
    }

    void FixedUpdate()
    {
        if (_st==2)
        {
            if (_dire_x==1)
            {
                _position.x += _speed;
            }
            else if (_dire_x == 2)
            {
                _position.x -= _speed;
            }
            if (_dire_y == 1)
            {
                _position.y += _speed;
            }
            else if (_dire_y == 2)
            {
                _position.y -= _speed;
            }
            transform.position = _position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        _st = 1;
        _dire_x = 0;
        _dire_y = 0;
        if (Input.GetKey("right"))
        {
            _st = 2;
            _dire_x = 1;
            _scale.x = 1;
        }
        else if (Input.GetKey("left"))
        {
            _st = 2;
            _dire_x = 2;
            _scale.x = -1;
        }
        transform.localScale = _scale;

        if (Input.GetKey("up"))
        {
            _st = 2;
            _dire_y = 1;
        }
        else if (Input.GetKey("down"))
        {
            _st = 2;
            _dire_y = 2;
        }
    }
}
