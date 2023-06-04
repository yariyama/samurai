using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandaManager : MonoBehaviour
{
    //モンキーオブジェクト
    private GameObject _Monkey;
    //モンキースクリプト
    private MonkeyManager _MonkeyManager;

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
    //_st=6-攻撃
    //_st=7-攻撃後

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
        if (_st == 1)
        {
            if (_vz != 0 || _angle != 0)
            {
                _st = 2;
                _animator.Play("Walk");
            }
        }
        else if (_st == 2)
        {
            if (_vz == 0 && _angle == 0)
            {
                _st = 1;
                _animator.Play("Base");
            }
        }
        else if (_st == 3)
        {
            _timer += Time.deltaTime;
            if (_timer >= 0.2f)
            {
                _timer = 0;
                _st = 4;
                _count = 0;
                _rbody.AddForce(new Vector3(0, _jump_p, 0), ForceMode.Impulse);
                _animator.Play("JumpUp");
            }
        }
        else if (_st == 4)
        {
            _timer += Time.deltaTime;
            if (_count == 0 && _timer >= 0.3f)
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
        else if (_st == 6)
        {
            _timer += Time.deltaTime;
            if (_timer >= 0.5f)
            {
                _timer = 0;
                _st = 7;
                _animator.Play("JumpDown");
                _rbody.constraints = RigidbodyConstraints.FreezeRotation;
            }
        }

        if (_st==2 || _st==4) {
            transform.Translate(0, 0, _vz / 50);
            transform.Rotate(0, _angle / 50, 0);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if ((_st==4||_st==7) && other.gameObject.tag=="Cube")
        {
            _st = 5;
            _timer = 0;
            _animator.Play("JumpArrive");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag=="Monkey" && _st==4 && _count==2)
        {
            _Monkey = collision.gameObject;
            _MonkeyManager = _Monkey.GetComponent<MonkeyManager>();
            if (_MonkeyManager._st<=3)
            {
                _st = 6;
                _timer =0;
                _animator.Play("JumpAttack");
                _rbody.constraints = RigidbodyConstraints.FreezePosition;

                _MonkeyManager.DameSet();
            }
        }
    }
}
