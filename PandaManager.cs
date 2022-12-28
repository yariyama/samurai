using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandaManager : MonoBehaviour
{
    //パンダメッシュオブジェクト
    private GameObject _PandaMesh;
    //モンキーオブジェクト
    private GameObject _Monkey;
    //モンキースクリプト
    private MonkeyManager _MonkeyManager;
    //メインカメラオブジェクト
    private GameObject _MainCamera;

    //リジッドボディ
    private Rigidbody _rbody;
    //アニメーター
    private Animator _animator;
    //コライダー
    private CapsuleCollider _collider;

    //パンダメッシュレンダラー
    private Renderer _panda_mesh_renderer;
    //パンダメッシュマテリアル
    private Material _panda_mesh_material;
    //パンダメッシュカラー
    private Color _panda_mash_color;

    //ステータス
    public int _st;
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
    //接地
    private bool _gound_st;


    //_st=1-基本形
    //_st=2-移動
    //_st=3-ジャンプセット
    //_st=4-ジャンプ
    //_st=5-ジャンプ着地
    //_st=6-ダメージ
    //_st=7-攻撃
    //_st=8-攻撃後

    void Awake()
    {
        _rbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _collider = GetComponent<CapsuleCollider>();

        _PandaMesh = transform.Find("PandaMesh").gameObject;
        _panda_mesh_renderer = _PandaMesh.GetComponent<Renderer>();
        _panda_mesh_material = _panda_mesh_renderer.material;
        _panda_mash_color = _panda_mesh_material.color;

        _MainCamera = GameObject.Find("Main Camera");
        //_MainCamera = transform.Find("Main Camera").gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _count = 0;
        _timer = 0;
        _push_st = false;
        _gound_st = true;
        _animator.Play("Base");

        _MainCamera.transform.parent = this.gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        _vz =0;
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
            if (_push_st==false && _gound_st) {
                _push_st = true;
                JumpSet();
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
                _st = 4;
                _timer = 0;
                _count =0;
                _rbody.AddForce(new Vector3(0,_jump_p,0),ForceMode.Impulse);
                _animator.Play("JumpUp");
            }
        }
        else if (_st==4)
        {
            _timer += Time.deltaTime;
            if (_count==0 && _timer>=0.3f)
            {
                _count = 1;
                _timer =0;
                _animator.Play("JumpTop");
            }
            else if (_count == 1 && _timer >= 0.3f)
            {
                _count = 2;
                _timer = 0;
                _animator.Play("JumpDown");
            }
        }
        else if (_st==5)
        {
            _timer += Time.deltaTime;
            if (_timer>=0.3f)
            {
                _st = 1;
                _timer =0;
                _animator.Play("Base");
            }
        }
        else if (_st==6)
        {
            _panda_mash_color.a -= 0.01f;
            if (_panda_mash_color.a<=0)
            {
                _panda_mash_color.a = 0;
                _panda_mesh_renderer.enabled = false;
            }
            _panda_mesh_material.color = _panda_mash_color;
        }
        else if (_st==7)
        {
            _timer += Time.deltaTime;
            if (_timer>=0.5f)
            {
                _timer =0;
                _st = 8;
                _animator.Play("JumpDown");
                _rbody.constraints = RigidbodyConstraints.FreezeRotation;
            }
        }

        if (_st==2 ||_st==4) {
            if (_vz != 0) {
                transform.Translate(0, 0, _vz / 50);
            }
            if (_angle != 0) {
                transform.Rotate(0, _angle / 50, 0);
            }
        }

        if (_MainCamera.transform.parent && !_gound_st && transform.position.y<=-6f)
        {
            _MainCamera.transform.parent = null;
        }
        else if (!_MainCamera.transform.parent && transform.position.y <= -10f)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void JumpSet()
    {
        _st = 3;
        _timer = 0;
        _animator.Play("JumpSet");
    }

    public void DamSet()
    {
        _st = 6;
        _animator.Play("StrengthDame");
        _collider.enabled = false;
        _rbody.constraints = RigidbodyConstraints.FreezePosition;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag=="Monkey"&& _st==4)
        {
            _Monkey = collision.gameObject;
            _MonkeyManager = _Monkey.GetComponent<MonkeyManager>();
            if (_MonkeyManager._st<=3)
            {
                _st = 7;
                _timer = 0;
                _animator.Play("JumpAttack");
                _rbody.constraints = RigidbodyConstraints.FreezePosition;

                _MonkeyManager.DameSet();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if ((_st==4||_st==8) && other.gameObject.tag=="Cloud")
        {
            _st = 5;
            _timer =0;
            _gound_st = true;
            _animator.Play("JumpArrive");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag=="Cloud")
        {
            _gound_st = false;
        }
    }
}
