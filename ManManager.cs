using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManManager : MonoBehaviour
{
    //アニメーター
    private Animator _animator;
    //リジッドボディ
    private Rigidbody _rbody;

    //移動スピード
    public float _w_speed;
    //回転スピード
    public float _a_speed;
    //移動量
    private float _vz;
    //回転量
    private float _angle;
    //ステータス
    private int _st;
    //タイマー
    private float _timer;
    //カウント
    private int _count;
    //ジャンプ力
    public float _jump_power;

    //_st=1-基本形
    //_st=2-移動
    //_st=3-ジャンプセット
    //_st=4-ジャンプ
    //_st=5-ジャンプ着地

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _rbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _animator.Play("Idle");
        _timer = 0;
        _count =0;
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
            if (_st==1||_st==2)
            {
                _st = 3;
                _timer =0;
                _animator.Play("JumpSet");
            }
        }
    }

    void FixedUpdate()
    {
        if (_st==1)
        {
            if (_vz!=0 || _angle!=0)
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
                _animator.Play("Idle");
            }
        }
        else if (_st==3)
        {
            _timer += Time.deltaTime;
            if (_timer>=0.3f)
            {
                _timer =0;
                _st = 4;
                _count = 0;
                _rbody.AddForce(new Vector3(0,_jump_power,0),ForceMode.Impulse);
                _animator.Play("JumpUp");
            }
        }
        else if (_st==4)
        {
            _timer += Time.deltaTime;
            if (_count==0 && _timer>=0.2f)
            {
                _timer = 0;
                _count = 1;
                _animator.Play("JumpTop");
            }
            else if (_count == 1 && _timer >= 0.2f)
            {
                _timer = 0;
                _count = 2;
                _animator.Play("JumpDown");
            }
        }
        else if (_st==5)
        {
            _timer += Time.deltaTime;
            if (_timer>=0.3f)
            {
                _timer =0;
                _st = 1;
                _animator.Play("Idle");
            }
        }

        if (_st==2||_st==4) {
            transform.Translate(0, 0, _vz / 50);
            transform.Rotate(0, _angle / 50, 0);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name=="Ground")
        {
            _st = 5;
            _timer =0;
            _animator.Play("JumpArrive");
        }
    }
}
