using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car5aManager : MonoBehaviour
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

    void FixedUpdate()
    {
        if (_st==2)
        {
            _position.x += _speed / 50;
            transform.position = _position;
        }
    }

    void OnMouseDown()
    {
        _st = 2;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
            _st = 1;
    }
}
