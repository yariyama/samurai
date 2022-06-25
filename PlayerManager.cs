using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //ジャンプセンサーオブジェクト
    private GameObject _JumpSensor;
    //ジャンプセンサースクリプト
    private JumpSensorManager _JumpSensorManager;

    //アニメーター
    private Animator _animator;
    //リジッドボディ
    private Rigidbody _rbody;
    //カプセルコライダー
    private CapsuleCollider _c_collider;

    //ステータス
    public int _st;
    //カウント
    private int _count;
    //歩きスピード
    public float _w_speed;
    //回転スピード
    public float _r_speed;
    //移動量
    private float _vz;
    //回転量
    private float _angle;
    //プッシュ状態
    private bool _push_st;
    //接地状態
    private bool _ground_st;


    //_st=1-基本形
    //_st=2-移動
    //_st=3-ジャンプ前
    //_st=4-ジャンプ
    //_st=5-ジャンプ後


    void Awake()
    {
        _JumpSensor = transform.Find("JumpSensor").gameObject;
        _JumpSensorManager = _JumpSensor.GetComponent<JumpSensorManager>();

        _animator = GetComponent<Animator>();
        _rbody = GetComponent<Rigidbody>();
        _c_collider = GetComponent<CapsuleCollider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _animator.Play("Idle");
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
            _angle = _r_speed;
        }
        else if (Input.GetKey("left"))
        {
            _angle = -_r_speed;
        }

        if (Input.GetKey("space") && (_st==1 || _st==2))
        {
            if (!_push_st && _ground_st)
            {
                _push_st = true;
                _st = 3;
                _count = 0;
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
                _animator.Play("Idle");
            }
        }
        else if (_st==3)
        {
            if (_count==0) {
                _animator.Play("Jump");
                _count = 1;
            }
        }

        if (_st==2||_st==4)
        {
            transform.Translate(0,0,_vz/50);
            transform.Rotate(0,_angle/50,0);
        }
    }

    void OnTriggerStay(Collider other)
    {
        _ground_st = true;
    }

    void OnTriggerExit(Collider other)
    {
        _ground_st = false;
    }

    void JumpSet(int _no)
    {
        if (_no==1)
        {
            _st = 4;
            _c_collider.center = new Vector3(0,1.3f,0);
            _c_collider.height = 1f;
            _rbody.constraints = RigidbodyConstraints.None;
            _rbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
        }
        else if (_no==2)
        {
            _st = 5;
            _c_collider.center = new Vector3(0, 0.9f, 0);
            _c_collider.height = 1.8f;
            _rbody.constraints = RigidbodyConstraints.None;
            _rbody.constraints = RigidbodyConstraints.FreezeRotation;
            _JumpSensorManager._st = 0;
        }
    }

    void JumpEnd()
    {
        _st = 1;
        _animator.Play("Idle");
    }

    void JumpSearch()
    {
        _JumpSensorManager._st = 1;
        if (!_ground_st)
        {
            _rbody.constraints = RigidbodyConstraints.None;
            _rbody.constraints = RigidbodyConstraints.FreezeRotation;
        }
    }

    public void UpSet()
    {
        _rbody.AddForce(new Vector3(0,5,0),ForceMode.Impulse);
        _rbody.constraints = RigidbodyConstraints.None;
        _rbody.constraints = RigidbodyConstraints.FreezeRotation;
    }
}
