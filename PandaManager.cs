using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandaManager : MonoBehaviour
{
    //アニメーター
    private Animator _animator;

    //スケール
    private Vector3 _scale;

    //ステータス
    private int _st;
    //移動量x
    private float _vx;
    //移動スピード
    public float _w_speed;
    //方向
    private int _dire;
    //タイマー
    private float _timer;

    //_st=1-基本形
    //_st=2-移動
    //_st=3-振り向き

    void Awake()
    {
        _animator = this.GetComponent<Animator>();
        _scale = this.transform.localScale;
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _dire = 1;
        _timer =0;
        _animator.Play("Base");
    }

    // Update is called once per frame
    void Update()
    {
        _vx = 0;
        if (Input.GetKey("right")|| Input.GetKey("d"))
        {
            if (_st<=2) {
                _vx = _w_speed;
                if (_dire == 2)
                {
                    TurnSet();
                }
            }
        }
        else if (Input.GetKey("left") || Input.GetKey("a"))
        {
            if (_st <= 2)
            {
                _vx = -_w_speed;
                if (_dire == 1)
                {
                    TurnSet();
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
                _animator.Play("Walk");
            }
        }
        else if (_st==2)
        {
            if (_vx == 0)
            {
                _st = 1;
                _animator.Play("Base");
            }
        }
        else if (_st==3)
        {
            _timer += Time.deltaTime;
            if (_timer>=0.1f)
            {
                _timer = 0;
                if (_dire==1)
                {
                    _dire = 2;
                }
                else
                {
                    _dire = 1;
                }
                _scale.x *= -1;
                transform.localScale = _scale;
                _st = 1;
                _animator.Play("Base");
            }
        }

        if (_st==2) {
            transform.Translate(_vx / 50, 0, 0);
        }
    }

    private void TurnSet()
    {
        _st = 3;
        _timer = 0;
        _animator.Play("Turn");
    }
}
