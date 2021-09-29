using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boy1Manager : MonoBehaviour
{
    //表示
    private SpriteRenderer _renderer;
    //スプライト
    public Sprite[] _spt = new Sprite[4];
    //座標
    private Vector2 _position;
    //スケール
    private Vector2 _scale;

    //ステータス
    private int _st;
    //タイマー
    private float _timer1;
    private float _timer2;
    //スピード
    public float _speed;
    //カウント
    private int _count;

    //_st=1-基本形
    //_st=2-移動

    void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _position = transform.position;
        _scale = transform.localScale;
    }

    void Start()
    {
        _renderer.sprite = _spt[0];

        _st = 1;
        _timer1 = 0;
        _timer2 = 0;
        _count = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_st==2)
        {
            //移動
            _timer1 += Time.deltaTime;
            if (_timer1>=3)
            {
                _timer1 = 0;
                _speed *= -1;
                _scale.x *= -1;
                transform.localScale = _scale;
            }
            //_position.x += _speed / 50;
            //transform.position = _position;
            transform.Translate(_speed/50,0,0);


            //アニメ
            _timer2 += Time.deltaTime;
            if (_timer2>=0.3f)
            {
                ++_count;
                if (_count==4)
                {
                    _count = 0;
                }
                _renderer.sprite = _spt[_count];
                _timer2 = 0;
                
            }
        }
    }

    void OnMouseDown()
    {
        if (_st==1)
        {
            _st = 2;
        }
        else
        {
            _st = 1;
            _renderer.sprite = _spt[0];
        }
    }
}
