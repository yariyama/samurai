using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeAreaManager : MonoBehaviour
{
    //表示
    private SpriteRenderer _renderer;
    //色
    private Color _color;

    //ステータス
    private int _st;
    //タイマー
    private float _timer;
    //プッシュステータス
    private bool _push_st;

    //_st=1-基本形
    //_st=2-アクション
    //_st=3-失敗

    void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _color = _renderer.color;
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _timer = 0;
        _push_st = false;
        _color.a = 0.4f;
        _renderer.color = _color;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("space"))
        {
            if (_st==1 && !_push_st)
            {
                _push_st = true;
                _st = 2;
                _timer = 0;
                _color.a = 0.7f;
                _renderer.color = _color;
            }
        }
        else
        {
            _push_st = false;
        }
    }

    void FixedUpdate()
    {
        //アクション
        if (_st==2)
        {
            _timer += Time.deltaTime;
            if (_timer>=0.1f)
            {
                _timer = 0;
                _st = 1;
                _color.a = 0.4f;
                _renderer.color = _color;
            }
        }
    }
}
