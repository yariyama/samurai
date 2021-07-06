using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car4Manager : MonoBehaviour
{
    //座標
    private Vector2 _position;
    //表示
    private SpriteRenderer _renderer;
    //色
    private Color _color;

    //ステータス
    private int _st;
    //スピード
    public float _speed;

    //_st=1-基本形
    //_st=2-移動
    //_st=3-消滅

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
    }

    void FixedUpdate()
    {
        if (_st==2)
        {
            //移動
            _position.x += _speed;
            transform.position = _position;
        }
        else if (_st==3)
        {
            //消滅
            _color.a -= 0.05f;
            if (_color.a<=0)
            {
                this.gameObject.SetActive(false);
            }
            _renderer.color = _color;
        }
    }

    void OnMouseDown()
    {
        _st = 2;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _st = 3;
    }
}
