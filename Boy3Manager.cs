using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boy3Manager : MonoBehaviour
{
    //表示
    private SpriteRenderer _renderer;
    //スケール
    private Vector2 _scale;
    //スプライト
    public Sprite[] _spt = new Sprite[2];

    //ステータス
    private int _st;
    //x方向
    private int _dire_x;
    //タイマー
    private float _timer;

    //_st=1-基本形
    //_st=2-振り向き

    //_dire=1-右
    //_dire=2-左

    void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _scale = transform.localScale;
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _dire_x = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("right"))
        {
            if (_st==1&&_dire_x==2)
            {
                _st = 2;
                _timer = 0;
            }
        }
        else if (Input.GetKey("left"))
        {
            if (_st == 1 && _dire_x == 1)
            {
                _st = 2;
                _timer = 0;
            }
        }
        
    }

    void FixedUpdate()
    {

        //振り向き
        if (_st==2)
        {
            if (_timer==0)
            {
                _renderer.sprite = _spt[1];
            }
            _timer += Time.deltaTime;
            if (_timer>=0.1f)
            {
                _timer = 0;
                _st = 1;
                _renderer.sprite = _spt[0];
                if (_dire_x==1)
                {
                    _dire_x = 2;
                    _scale.x = -1;
                }
                else
                {
                    _dire_x = 1;
                    _scale.x = 1;
                }
                transform.localScale = _scale;
            }
        }
    }
}
