using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandaManager : MonoBehaviour
{
    //アニメーター
    private Animator _animtor;
    //リジッドボディ
    private Rigidbody _rbody;

    //パンダメッシュオブジェクト
    private GameObject _PandaMesh;
    //パンダメッシュコライダー
    private CapsuleCollider _panda_mesh_collider;

    //パンダメッシュレンダラー
    private Renderer _panda_mesh_renderer;
    //パンダメッシュマテリアル
    private Material _panda_mesh_material;
    //パンダメッシュカラー
    private Color _panda_mesh_color;

    //メインカメラオブジェクト
    private GameObject _MainCamera;

    //スピード
    public float _speed;
    //移動量x
    //private float _vx;
    //移動量z
    private float _vz;
    //回転量
    private float _angle;
    //回転スピード
    public float _a_speed;
    //ジャンプパワー
    public float _jump_p;

    //ステータス
    public int _st;
    //カウント
    private int _count;
    //タイマー
    private float _timer;

    //プッシュ有無
    private bool _push_st;
    //接地有無
    private bool _ground_st;

    //モンキーオブジェクト
    private GameObject _Monkey;
    //モンキースクリプト
    private MonkeyManager _MonkeyManager;

    //_st=1-基本形
    //_st=2-移動
    //_st=3-ジャンプ前
    //_st=4-ジャンプ
        //_count=0-上昇
        //_count=1-トップ
        //_count=2-下降
    //_st=5-ジャンプ後
    //_st=6-攻撃
    //_st=7-攻撃後
    //_st=8-ダメージ

    void Awake()
    {
        //定義
        _animtor = this.GetComponent<Animator>();
        _rbody = this.GetComponent<Rigidbody>();

        _PandaMesh = transform.Find("PandaMesh").gameObject;
        _panda_mesh_collider = _PandaMesh.GetComponent<CapsuleCollider>();
        _panda_mesh_renderer = _PandaMesh.GetComponent<Renderer>();
        _panda_mesh_material = _panda_mesh_renderer.material;
        _panda_mesh_color = _panda_mesh_material.color;

        _MainCamera = GameObject.Find("Main Camera");
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _ground_st = true;
        _animtor.Play("Base");
    }

    // Update is called once per frame
    void Update()
    {
        //_vx = 0;
        _vz = 0;
        _angle = 0;

        if (Input.GetKey("up") || Input.GetKey("w"))
        {
            _vz = _speed;
        }
        else if (Input.GetKey("down") || Input.GetKey("s"))
        {
            _vz = -_speed;
        }
        if (Input.GetKey("right") || Input.GetKey("d"))
        {
            //_vx = _speed;
            _angle = _a_speed;
        }
        else if (Input.GetKey("left") || Input.GetKey("a"))
        {
            //_vx = -_speed;
            _angle = -_a_speed;
        }

        if (Input.GetKey("space"))
        {
            if ((_st==1||_st==2)&&_push_st==false && _ground_st==true)
            {
                _push_st = true;
                _st = 3;
                _timer =0;
                _animtor.Play("JumpSet");
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
            if (_angle!=0 || _vz!=0)
            {
                _st = 2;
                _animtor.Play("Walk");
            }
        }
        else if (_st==2)
        {
            if (_angle == 0 && _vz == 0)
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
                _timer = 0;
                _st = 4;
                _count = 0;
                _rbody.AddForce(new Vector3(0,_jump_p,0),ForceMode.Impulse);
                _animtor.Play("JumpUp");
            }
        }
        else if (_st==4)
        {
            _timer += Time.deltaTime;
            if (_count==0 && _timer>=0.2f)
            {
                _timer = 0;
                _count = 1;
                _animtor.Play("JumpTop");
            }
            else if (_count==1 && _timer>=0.2f)
            {
                _timer = 0;
                _count = 2;
                _animtor.Play("JumpDown");
            }
        }
        else if (_st == 5)
        {
            _timer += Time.deltaTime;
            if (_timer>=0.3f)
            {
                _timer = 0;
                _st = 1;
                _animtor.Play("Base");
            }
        }
        else if (_st==6)
        {
            _timer += Time.deltaTime;
            if (_timer >= 1f)
            {
                _timer = 0;
                _st = 7;
                _animtor.Play("JumpDown");
                _rbody.constraints = RigidbodyConstraints.FreezeRotation;
            }
        }
        else if (_st==8)
        {
            _panda_mesh_color.a -= 0.01f;
            if (_panda_mesh_color.a<=0)
            {
                _panda_mesh_color.a = 0;
                _MainCamera.transform.parent = null;
                this.gameObject.SetActive(false);
            }
            _panda_mesh_material.color = _panda_mesh_color;
        }

        if (_st==2 || _st==4)
        {
            this.transform.Translate(0, 0, _vz / 50);
            this.transform.Rotate(0,_angle/50,0);
        }
    }

    public void DameSet()
    {
        _st = 8;
        _animtor.Play("StrengthDame");
        _panda_mesh_collider.enabled = false;
        _rbody.constraints = RigidbodyConstraints.FreezePosition;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag=="Monkey" && _st==4)
        {
            _Monkey = collision.gameObject;
            _MonkeyManager = _Monkey.GetComponent<MonkeyManager>();

            _st = 6;
            _timer = 0;
            _animtor.Play("JumpAttack");
            _rbody.constraints = RigidbodyConstraints.FreezePosition;

            _MonkeyManager.DameSet();
        }
        else if (collision.gameObject.tag == "Cube" && (_st == 4||_st==7))
        {
            _st = 5;
            _timer = 0;
            _animtor.Play("JumpArrive");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Cube")
        {
            _ground_st = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Cube")
        {
            _ground_st = false;
        }
    }
}
