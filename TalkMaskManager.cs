using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkMaskManager : MonoBehaviour
{
    //座標
    private Vector2 _position;

    //ステータス
    public int _st;
    //アウトセットx
    public float _out_set_x;
    //インセットx
    public float _in_set_x;
    //スピード
    public float _speed;

    //_st=1-基本形
    //_st=2-待機
    //_st=3-スライドイン
    //_st=4-スライドアウト

    void Awake()
    {
        _position = transform.localPosition;
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 2;
        _position.x = _out_set_x;
        transform.localPosition = _position;
    }

    void FixedUpdate()
    {
        if (_st==3)
        {
            _position.x += _speed;
            if (_position.x>=_in_set_x)
            {
                _position.x = _in_set_x;
                _st = 1;
            }
            transform.localPosition = _position;
        }
        else if (_st==4)
        {
            _position.x -= _speed;
            if (_position.x <= _out_set_x)
            {
                _position.x = _out_set_x;
                _st = 2;
            }
            transform.localPosition = _position;
        }
    }

    //スライドセット
    public void SlideSet(int _no)
    {
        if (_no==1)
        {
            _st = 3;
        }
        else if (_no==2)
        {
            _st = 4;
        }
        _position = transform.localPosition;
    }
}
