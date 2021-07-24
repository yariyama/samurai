using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Girl1Manager : MonoBehaviour
{
    //座標
    private Vector2 _position;
    //アニメーター
    public Animator _animator;

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
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _animator.Play("Base");
        
    }

    void FixedUpdate()
    {
        if (_st == 2)
        {
            if (_dire_x == 1)
            {
                _position.x += _speed / 50;
                if (_position.x >= 6.2f)
                {
                    _position.x = 6.2f;
                }
            }
            else if (_dire_x == 2)
            {
                _position.x -= _speed / 50;
                if (_position.x <= -6.2f)
                {
                    _position.x = -6.2f;
                }
            }
            if (_dire_y == 1)
            {
                _position.y += _speed / 50;
                if (_position.y >= 4.3f)
                {
                    _position.y = 4.3f;
                }
            }
            else if (_dire_y == 2)
            {
                _position.y -= _speed / 50;
                if (_position.y <= -4.3f)
                {
                    _position.y = -4.3f;
                }
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
        //animator.Play("Base");
        if (Input.GetKey("right"))
        {
            _st = 2;
            _dire_x = 1;
            _animator.Play("Right");
        }
        else if (Input.GetKey("left"))
        {
            _st = 2;
            _dire_x = 2;
            _animator.Play("Left");
        }

        if (Input.GetKey("up"))
        {
            _st = 2;
            _dire_y = 1;
            if (_dire_x==0) {
                _animator.Play("Up");
            }
        }
        else if (Input.GetKey("down"))
        {
            _st = 2;
            _dire_y = 2;
            if (_dire_x == 0)
            {
                _animator.Play("Down");
            }
        }

        if (_st==1)
        {
            _animator.Play("Base");
        }
    }
}
