using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandaManager : MonoBehaviour
{
    //リジッドボディ
    private Rigidbody _rbody;
    //アニメーター
    private Animator _animator;

    //ステータス
    private int _st;
    //カウント
    private int _count;
    //タイマー
    private float _timer;
    //移動量z
    private float _vz;
    //移動スピード
    public float _w_speed;
    //回転量
    private float _angle;
    //回転スピード
    public float _a_speed;
    //ジャンプ力
    public float _jump_p;
    //プッシュ有無
    private bool _push_st;

    //_st=1-基本形
    //_st=2-移動
    //_st=3-ジャンプセット
    //_st=4-ジャンプ
    //_st=5-ジャンプ着地

    void Awake()
    {
        _rbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _count = 0;
        _timer = 0;
        _animator.Play("Base");
    }

    // Update is called once per frame
    void Update()
    {
        _vz = 0;
        _angle = 0;
        if (Input.GetKey("up"))
        {
            _vz = _w_speed;
        }
        else if (Input.GetKey("down"))
        {
            _vz = -_w_speed;
        }
        if (Input.GetKey("right"))
        {
            _angle = _a_speed;
        }
        else if (Input.GetKey("left"))
        {
            _angle = -_a_speed;
        }

        if (Input.GetKey("space"))
        {
            if (!_push_st)
            {
                _push_st = true;
                _st = 3;
                _timer = 0;
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
            if (_vz!=0||_angle!=0)
            {
                _st = 2;
                _animator.Play("Walk");
            }
        }
        else if (_st==2)
        {
            if (_vz == 0 && _angle == 0)
            {
                _st = 1;
                _animator.Play("Base");
            }
        }
        else if (_st==3)
        {
            _timer += Time.deltaTime;
            if (_timer>=0.2f)
            {
                _timer =0;
                _st = 4;
                _count = 0;
                _rbody.AddForce(new Vector3(0,_jump_p,0),ForceMode.Impulse);
                _animator.Play("JumpUp");
            }
        }
        else if (_st == 4)
        {
            _timer += Time.deltaTime;
            if (_count==0 && _timer>=0.3f)
            {
                _timer = 0;
                _count = 1;
                _animator.Play("JumpTop");
            }
            else if (_count==1 && _timer >= 0.2f)
            {
                _timer = 0;
                _count = 2;
                _animator.Play("JumpDown");
            }
        }
        else if (_st == 5)
        {
            _timer += Time.deltaTime;
            if (_timer >= 0.3f)
            {
                _timer = 0;
                _st = 1;
                _animator.Play("Base");
            }
        }

        if (_st==2 || _st==4) {
            transform.Translate(0, 0, _vz / 50);
            transform.Rotate(0, _angle / 50, 0);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (_st==4 && other.gameObject.tag=="Cube")
        {
            _st = 5;
            _timer = 0;
            _animator.Play("JumpArrive");
        }
    }
}
