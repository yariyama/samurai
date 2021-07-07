using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car4Manager : MonoBehaviour
{
    //座標
    private Vector2 _position;

    //ステータス
    private int _st;
    //スピード
    public float _speed;

    //_st=1-基本形
    //_st=2-移動

    void Awake()
    {
        _position = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_st==1)
        {
            _position.x = -7.5f;
            _position.y = Random.Range(-4.5f, 4.5f);
            transform.position = _position;
            _st = 2;
        }
        else if (_st==2)
        {
            _position.x += _speed / 50;
            if (_position.x>=7.5f)
            {
                _st = 1;
            }
            transform.position = _position;
        }
    }
}
