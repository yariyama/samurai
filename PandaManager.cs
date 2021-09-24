using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandaManager : MonoBehaviour
{
    //リジッドボディ
    private Rigidbody2D _rbody;
    //アニメーター
    private Animator _animator;
    //スケール
    private Vector2 _scale;

    //ステータス
    private int _st;
    //移動スピード
    public float _w_speed;
    //x移動量
    private float _vx;
    //x方向
    private int _dire_x;
    //タイマー
    private float _timer;


    //_st=1-基本形
    //_st=2-移動
    //_st=3-振り向き

    void Awake()
    {
        _rbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _scale = transform.localScale;
    }


    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _dire_x = 1;
        _timer = 0;
        _animator.Play("base");
    }

    // Update is called once per frame
    void Update()
    {
        _vx = 0;

        if (Input.GetKey("right"))
        {
            _vx = _w_speed;
            if (_dire_x==2)
            {
                if (_st==1||_st==2)
                {
                    _st = 3;
                    _timer = 0;
                }
            }
        }
        else if (Input.GetKey("left"))
        {
            _vx = -_w_speed;
            if (_dire_x == 1)
            {
                if (_st == 1 || _st == 2)
                {
                    _st = 3;
                    _timer = 0;
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (_st==1)
        {
            if (_vx!=0)
            {
                _st = 2;
                _animator.Play("walk");
            }
        }
        else if (_st == 2)
        {
            if (_vx == 0)
            {
                _st = 1;
                _animator.Play("base");
            }
        }
        else if (_st==3)
        {
            if (_timer==0)
            {
                _animator.Play("turn");
            }
            _timer += Time.deltaTime;
            if (_timer >= 0.05f)
            {
                _timer = 0;
                _st = 1;
                if (_dire_x == 1)
                {
                    _dire_x = 2;
                    _scale.x = -1.3f;
                }
                else
                {
                    _dire_x = 1;
                    _scale.x = 1.3f;
                }
                _animator.Play("base");
                transform.localScale = _scale;
            }
        }



        if (_vx!=0)
        {
            //_rbody.velocity = new Vector2(_vx,0);
            transform.Translate(_vx/50,0,0);
        }
    }
}
