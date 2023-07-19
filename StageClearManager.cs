using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClearManager : MonoBehaviour
{
    //座標
    private Vector2 _position;

    //ステータス
    public int _st;

    //_st=1-基本形
    //_st=2-スライド

    void Awake()
    {
        _position = transform.localPosition;
    }

    // Start is called before the first frame update
    void Start()
    {
        //_st = 0;
    }

    void FixedUpdate()
    {
        if (_st==2)
        {
            _position.x -= 30;
            if (_position.x<=0)
            {
                _position.x = 0;
                _st = 1;
            }
            transform.localPosition = _position;
        }
    }

    public void ActiveSet()
    {
        _st = 2;
        _position.x = 770;
        transform.localPosition = _position;
    }
}
