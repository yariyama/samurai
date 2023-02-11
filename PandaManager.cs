using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandaManager : MonoBehaviour
{
    //モンキーオブジェクト
    private GameObject _Monkey;
    //モンキースクリプト
    private MonkeyManager _MonkeyManager;

    //アニメーター
    private Animator _animtor;
    //リジッドボディ
    private Rigidbody _rbody;
    //コライダー
    private BoxCollider _collider;

    //パンダメッシュオブジェクト
    private GameObject _PandaMesh;
    //パンダメッシュ表示
    private SkinnedMeshRenderer _panda_mesh_renderer;

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
    //回転量
    private float _angle;
    //回転スピード
    public float _angle_speed;

    //ステータス
    public int _st;
    //カウント
    private int _count;

    //_st=1-基本形
    //_st=2-回転
    //_st=3-ジャンプ前
    //_st=4-ジャンプ
    //_st=5-着地
    //_st=6-ダメージ
    //_st=7-攻撃
    //_st=8-攻撃後

    void Awake()
    {
        _animtor = GetComponent<Animator>();
        _rbody = GetComponent<Rigidbody>();
        _collider = GetComponent<BoxCollider>();

        _PandaMesh = transform.Find("PandaMesh").gameObject;
        _panda_mesh_renderer = _PandaMesh.GetComponent<SkinnedMeshRenderer>();
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
        //_vx = 0;
        _vz = 0;
        _angle = 0;

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
            //_vx = _speed;
            _angle = _angle_speed;
        }
        else if (Input.GetKey("left"))
        {
            //_vx = -_speed;
            _angle = -_angle_speed;
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
            if (_vz!=0 || _angle!=0)
            {
                _st = 2;
                _animtor.Play("Walk");
            }
        }
        else if (_st==2)
        {
            if (_vz == 0 && _angle==0)
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
            _timer += Time.deltaTime;
            if (_count==0)
            {
                if (_timer>=0.2f)
                {
                    _timer = 0;
                    _count = 1;
                    _animtor.Play("JumpTop");
                }
            }
            else if (_count == 1)
            {
                if (_timer >= 0.2f)
                {
                    _timer = 0;
                    _count = 2;
                    _animtor.Play("JumpDown");
                }
            }
        }
        else if (_st==5)
        {
            _timer += Time.deltaTime;
            if (_timer>=0.3f)
            {
                _timer =0;
                _st = 1;
                _animtor.Play("Base");
            }
        }
        else if (_st==6)
        {
            _timer += Time.deltaTime;
            if (_timer>=1)
            {
                _timer = 0;
                _st = 0;
                _panda_mesh_renderer.enabled = false;
            }
        }
        else if (_st==7)
        {
            _timer += Time.deltaTime;
            if (_timer>=0.5f)
            {
                _timer = 0;
                _st = 8;
                _animtor.Play("JumpDown");
                _rbody.constraints = RigidbodyConstraints.FreezeRotation;
            }
        }

        if (_st==1||_st==2||_st==4) {
            transform.Translate(0, 0, _vz / 50);
            transform.Rotate(0,_angle/50,0);
        }
    }

    public void DameSet()
    {
        _st = 6;
        _timer = 0;
        _animtor.Play("StrengthDame");
        _collider.enabled = false;
        _rbody.constraints = RigidbodyConstraints.FreezePosition;
    }

    void OnTriggerEnter(Collider other)
    {
        if ((_st==4||_st==8) && other.gameObject.tag=="Cube")
        {
            _st = 5;
            _timer =0;
            _animtor.Play("JumpArrive");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag=="Monkey" && _st==4)
        {
            _Monkey = collision.gameObject;
            _MonkeyManager = _Monkey.GetComponent<MonkeyManager>();
            if (_MonkeyManager._st<=3)
            {
                _st = 7;
                _timer = 0;
                _animtor.Play("JumpAttack");
                _rbody.constraints = RigidbodyConstraints.FreezePosition;

                _MonkeyManager.DameSet();
            }
        }
    }
}
