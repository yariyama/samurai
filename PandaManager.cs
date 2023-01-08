using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandaManager : MonoBehaviour
{
    //アニメーター
    private Animator _animtor;
    //リジッドボディ
    private Rigidbody _rbody;

    //移動量x
    private float _vx;
    //移動量z
    private float _vz;
    //スピード
    public float _speed;
    //ジャンプ力
    public float _jump_power;
    //プッシュ有無
    private bool _push_st;
    //タイマー
    private float _timer;

    //ステータス
    private int _st;
    //カウント
    private int _count;

    //_st=1-基本形
    //_st=2-回転
    //_st=3-ジャンプ前
    //_st=4-ジャンプ
    //_st=5-着地

    void Awake()
    {
        _animtor = GetComponent<Animator>();
        _rbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _animtor.Play("Base");
    }

    // Update is called once per frame
    void Update()
    {
        _vx = 0;
        _vz = 0;

        if (Input.GetKey("up"))
        {
            _vz = _speed;
        }
        else if (Input.GetKey("down"))
        {
            _vz = -_speed;
        }

        if (Input.GetKey("right"))
        {
            _vx = _speed;
        }
        else if (Input.GetKey("left"))
        {
            _vx = -_speed;
        }

        if (Input.GetKey("space"))
        {
            if (_push_st==false) {
                _push_st = true;
                _st = 3;
                _timer = 0;
                _animtor.Play("JumpSet");
                //_rbody.AddForce(new Vector3(0, _jump_power, 0), ForceMode.Impulse);
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
            if (_vx!=0 || _vz!=0)
            {
                _st = 2;
                _animtor.Play("Walk");
            }
        }
        else if (_st==2)
        {
            if (_vx == 0 && _vz == 0)
            {
                _st = 1;
                _animtor.Play("Base");
            }
        }
        else if (_st==3)
        {
            _timer += Time.deltaTime;
            if (_timer>=0.2f)
            {
                _timer =0;
                _st = 4;
                _count =0;
                _animtor.Play("JumpUp");
                _rbody.AddForce(new Vector3(0, _jump_power, 0), ForceMode.Impulse);
            }
        }
        else if (_st==4)
        {

        }
        else if (_st==5)
        {

        }

        transform.Translate(_vx / 50, 0, _vz / 50);
    }
}
