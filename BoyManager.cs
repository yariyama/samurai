using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoyManager : MonoBehaviour
{
    //エネミーオブジェクト
    private GameObject _Enemy;
    //エネミースクリプト
    private EnemyManager _EnemyManager;
    //ボーイメッシュ
    private GameObject _BoyMesh;
    //メインカメラオブジェクト
    private GameObject _MainCamera;


    //表示
    private SkinnedMeshRenderer _renderer;
    //マテリアル
    private Material _material;
    //色
    private Color _color;

    //リジッドボディ
    private Rigidbody _rbody;
    //アニメーター
    private Animator _animator;

    //ステータス
    public int _st;
    //移動スピード
    public float _speed;
    //移動量
    private float _vx;
    private float _vz;

    //ジャンプ力
    public float _jump_power;
    //押されている有無
    private bool _push_st;
    //接地有無
    private bool _ground_st;
    //タイマー
    private float _timer;

    //_st=1-基本形
    //_st=2-移動
    //_st=3-ジャンプ溜め
    //_st=4-ジャンプ
    //_st=5-ダメージ

    void Awake()
    {
        _rbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();

        _BoyMesh = transform.Find("BoyMesh").gameObject;
        _renderer = _BoyMesh.GetComponent<SkinnedMeshRenderer>();
        _material = _renderer.material;
        _color = _material.color;

        _MainCamera = transform.Find("Main Camera").gameObject;
    }


    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _push_st = false;
        _ground_st = true;
        _animator.Play("Base");
    }

    // Update is called once per frame
    void Update()
    {
        _vx = 0;
        _vz = 0;

        //縦移動
        if (Input.GetKey("up"))
        {
            _vz = _speed;
        }
        else if (Input.GetKey("down"))
        {
            _vz = -_speed;
        }
        //横移動
        if (Input.GetKey("right"))
        {
            _vx = _speed;
        }
        else if (Input.GetKey("left"))
        {
            _vx = -_speed;
        }

        //ジャンプ
        if (Input.GetKey("space"))
        {
            if (!_push_st && _ground_st)
            {
                _push_st = true;
                _st = 3;
            }
        }
        else
        {
            _push_st = false;
        }
    }

    private void FixedUpdate()
    {
        if (_st==1)
        {
            if (_vx!=0||_vz!=0)
            {
                _st = 2;
                _animator.Play("Walk");
            }
        }
        else if (_st==2)
        {
            if (_vx == 0 && _vz== 0)
            {
                _st = 1;
                _animator.Play("Base");
            }
        }
        else if (_st==3)
        {
            _st = 4;
            _rbody.AddForce(new Vector3(0,_jump_power,0),ForceMode.Impulse);
            _animator.Play("BaseJump");
        }
        else if (_st==5)
        {
            _color.a -= 0.01f;
            if (_color.a<=0)
            {
                _st = 0;

                if (_MainCamera.transform.parent==true)
                {
                    _MainCamera.transform.parent = null;
                }
                this.gameObject.SetActive(false);
            }
            _material.color=_color;
        }

        if (_st==2 || _st==4) {
            transform.Translate(_vx / 50, 0, _vz / 50);
        }

        if (!_ground_st)
        {
            if (transform.position.y<0 && _MainCamera.transform.parent)
            {
                _MainCamera.transform.parent = null;
            }
            if (transform.position.y<-20 && _st!=0)
            {
                _st = 0;
                this.gameObject.SetActive(false);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Enemy" && _st==4)
        {
            _Enemy = other.gameObject;
            _EnemyManager = _Enemy.GetComponent<EnemyManager>();
            if (_EnemyManager._st==1||_EnemyManager._st==2)
            {
                _EnemyManager.DameSet();
            }
        }
        else if (other.gameObject.tag=="Cube")
        {
            _ground_st = true;

            if (_st==4)
            {
                _st = 1;
                _animator.Play("JumpBase");
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Cube")
        {
            _ground_st = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag=="Enemy" && _st!=5)
        {
            _Enemy = collision.gameObject;
            _EnemyManager = _Enemy.GetComponent<EnemyManager>();

            if (_EnemyManager._st==1|| _EnemyManager._st == 2)
            {
                _st = 5;
                _timer = 0;
                _animator.Play("BaseDame");
            }
        }
    }
}
