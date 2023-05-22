using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandaManager : MonoBehaviour
{
    //アニメーター
    private Animator _animator;
    //リジッドボディ
    private Rigidbody2D _rbody;

    //スケール
    private Vector3 _scale;

    //ステータス
    private int _st;
    //カウント
    private int _count;
    //移動量x
    private float _vx;
    //移動スピード
    public float _w_speed;
    //方向
    private int _dire;
    //タイマー
    private float _timer;
    //ジャンプ力
    public float _jump_p;
    //プッシュ有無
    private bool _push_st;

    //_st=1-基本形
    //_st=2-移動
    //_st=3-振り向き
    //_st=4-ジャンプ前
    //_st=5-ジャンプ
    //_st=6-ジャンプ後

    void Awake()
    {
        _animator = this.GetComponent<Animator>();
        _rbody = GetComponent<Rigidbody2D>();
        _scale = this.transform.localScale;
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _dire = 1;
        _timer =0;
        _count = 0;
        _animator.Play("Base");
    }

    // Update is called once per frame
    void Update()
    {
        _vx = 0;
        if (Input.GetKey("right")|| Input.GetKey("d"))
        {
            if (_st<=2||_st==5) {
                _vx = _w_speed;
                if (_dire == 2)
                {
                    TurnSet();
                }
            }
        }
        else if (Input.GetKey("left") || Input.GetKey("a"))
        {
            if (_st <= 2 || _st == 5)
            {
                _vx = -_w_speed;
                if (_dire == 1)
                {
                    TurnSet();
                }
            }
        }

        if (Input.GetKey("space"))
        {
            if (_push_st==false && (_st==1 ||_st==2))
            {
                _push_st = true;
                _st = 4;
                _timer =0;
                _animator.Play("JumpSet");
            }
        }
        else
        {
            _push_st = false;
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
        else if (_st==4)
        {
            _timer += Time.deltaTime;
            if (_timer>=0.2f)
            {
                _timer = 0;
                _st = 5;
                _count = 0;
                _animator.Play("JumpUp");
                _rbody.AddForce(new Vector3(0,_jump_p,0),ForceMode2D.Impulse);
            }
        }
        else if (_st == 5)
        {
            _timer += Time.deltaTime;
            if (_count==0)
            {
                if (_timer>=0.2f)
                {
                    _timer = 0;
                    _count = 1;
                    _animator.Play("JumpTop");
                }
            }
            else if (_count==1)
            {
                if (_timer >= 0.2f)
                {
                    _timer = 0;
                    _count = 2;
                    _animator.Play("JumpDown");
                }
            }
        }
        else if (_st == 6)
        {
            _timer += Time.deltaTime;
            if (_timer>=0.2f)
            {
                _timer =0;
                _st = 1;
                _animator.Play("Base");
            }
        }

        if (_st==2||_st==5) {
            transform.Translate(_vx / 50, 0, 0);
        }
    }

    private void TurnSet()
    {
        _st = 3;
        _timer = 0;
        _animator.Play("Turn");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Cloud" && _st==5)
        {
            _st = 6;
            _timer = 0;
            _animator.Play("JumpSet");
            Debug.Log(100);
        }
    }
}
