using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obake1Manager : MonoBehaviour
{
    //座標
    private Vector2 _position;
    //表示
    private SpriteRenderer _renderer;
    //色
    private Color _color;

    //ステータス
    private int _st;
    //方向
    private int _dire;
    //スピード
    public float _speed;
    //カウント
    private int _count;
    //タイマー
    private float _timer;

    //_st=1-基本形
    //_st=2-移動
    //_st=3-ダメージ

    //_dire=1-右
    //_dire=2-左

    void Awake()
    {
        _position = transform.position;
        _renderer = GetComponent<SpriteRenderer>();
        _color = _renderer.color;
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _speed = 1;
        _dire = 1;
    }

    void FixedUpdate()
    {
        if (_st==2)
        {
            if (_dire==1)
            {
                _position.x += _speed / 50;
            }
            else
            {
                _position.x -= _speed / 50;
            }
            transform.position = _position;
        }
        else if (_st==3)
        {
            //点滅
            if (_count==0)
            {
                _color.g -= 0.1f;
                _color.b -= 0.1f;
                if (_color.g<=0)
                {
                    _color.g = 0;
                    _color.b = 0;
                    _count = 1;
                }
            }
            else if (_count == 1)
            {
                _color.g += 0.1f;
                _color.b += 0.1f;
                if (_color.g >= 1)
                {
                    _color.g = 1;
                    _color.b = 1;
                    _count = 0;
                }
            }

            //タイマーカウント
            _timer += Time.deltaTime;
            if (_timer>=2)
            {
                _st = 2;
                if (_dire==1)
                {
                    _dire = 2;
                }
                else
                {
                    _dire = 1;
                }
                _color.g = 1;
                _color.b = 1;
            }

            _renderer.color = _color;
        }
    }

    void OnMouseDown()
    {
        if (_st==1)
        {
            _st = 2;
        }
        else if (_st==2)
        {
            _st = 1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _st = 3;
        _timer = 0;
        _count = 0;
    }
}
