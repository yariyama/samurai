using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    //回転スクリプト
    private RotateManager _RotateManager;
    //FPSキャラクターオブジェクト
    //private GameObject _FPS_Character;
    //FPSキャラクタースクリプト
    //private FPS_CharacterManager _FPS_CharacterManager;
    //テゥーンソルジャーデモオブジェクト
    private GameObject _ToonSoldier_demo;
    //テゥーンソルジャーデモスクリプト
    private ToonSoldier_demoManager _ToonSoldier_DemoManager;

    //弾痕プレハブ
    public GameObject _BulletHolePrefab;
    //弾痕オブジェクト
    private GameObject _BulletHole;
    //サブカメラ
    private GameObject _SubCamera;
    //コライダー
    private BoxCollider _collider;

    //アニメーター
    private Animator _animator;
    //座標
    private Vector3 _position;
    //サブカメラ座標
    private Vector3 _sub_camera_position;
    //サブカメラ正面角度
    private Vector3 _sub_camera_forword_angle;

    //ステータス
    public int _st;
    //バージョン
    public int _ver;
    //移動スピード
    public float _speed;
    //移動量X
    private float _vx;
    //移動量Z
    private float _vz;

    //_st=1-基本形
    //_st=2-移動
    //_st=3-ショット
    //_st=4-ダメージ

    void Awake()
    {
        _RotateManager = GetComponent<RotateManager>();
        //_FPS_Character = transform.Find("FPS_Character").gameObject;
        //_FPS_CharacterManager = _FPS_Character.GetComponent<FPS_CharacterManager>();
        _ToonSoldier_demo = transform.Find("ToonSoldier_demo").gameObject;
        _ToonSoldier_DemoManager = _ToonSoldier_demo.GetComponent<ToonSoldier_demoManager>();
        _SubCamera = transform.Find("SubCamera").gameObject;

        _animator = GetComponent<Animator>();
        _position = transform.position;
        _collider = GetComponent<BoxCollider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _vx = 0;
        _vz = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (_st!=4) {
            _vx = 0;
            _vz = 0;

            if (Input.GetKey("right"))
            {
                _vx = _speed;
            }
            else if (Input.GetKey("left"))
            {
                _vx = -_speed;
            }
            if (Input.GetKey("up"))
            {
                _vz = _speed;
            }
            else if (Input.GetKey("down"))
            {
                _vz = -_speed;
            }

            if (Input.GetKey("space"))
            {
                if (_st == 1)
                {
                    _st = 3;
                    //_animator.Play("Character_AimPose");
                    _animator.SetBool("shoot_flag", true);
                    //_FPS_CharacterManager.ShootSet();
                    _ToonSoldier_DemoManager.ShootSet();
                    BulletHoleSet();
                }
                _RotateManager.ShootRot();
            }
        }
    }

    void FixedUpdate()
    {
        if (_st==1)
        {
            if (_vx!=0||_vz!=0)
            {
                _st = 2;
                //_animator.Play("Character_Walk");
                _animator.SetBool("walk_flag",true);
            }
        }
        else if (_st==2)
        {
            if (_vx==0 && _vz==0)
            {
                BaseSet();
            }
        }

        //移動
        if (_vx != 0 || _vz != 0)
        {
            transform.Translate(_vx/50,0,_vz/50);
            _position = transform.position;
            _position.y = 0;
            transform.position = _position;
        }
    }

    public void BaseSet()
    {
        _st = 1;
        //_animator.Play("Character_Idle");
        _animator.SetBool("walk_flag", false);
        _animator.SetBool("shoot_flag", false);
    }

    public void ActiveSet(int _no)
    {
        _ver = _no;
        if (_ver==2)
        {
            transform.position = new Vector3(0,0,5);
            transform.localEulerAngles = new Vector3(0,180,0);
            _RotateManager._angle = transform.localEulerAngles;
        }
    }

    private void BulletHoleSet()
    {
        RaycastHit hit;

        _sub_camera_position = _SubCamera.transform.position;
        _sub_camera_forword_angle = _SubCamera.transform.forward;

        Ray ray = new Ray(_sub_camera_position, _sub_camera_forword_angle);

        //レイ確認用
        Debug.DrawRay(ray.origin,ray.direction*10,Color.red,5);

        if (Physics.Raycast(ray,out hit,Mathf.Infinity))
        {
            //_BulletHole = Instantiate(_BulletHolePrefab);
            _BulletHole = PhotonNetwork.Instantiate("Prefabs/bulletHole", Vector3.zero, Quaternion.identity, 0);
            //位置
            _BulletHole.transform.position = hit.point;
            //角度
            _BulletHole.transform.rotation = Quaternion.FromToRotation(Vector3.forward, hit.normal);
        }
    }

    public void DameSet()
    {
        _st = 4;
        _animator.SetBool("dame_trigger",true);
        _collider.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (_st!=4)
        {
            if (other.gameObject.tag=="BulletHole")
            { 
                other.gameObject.transform.parent = this.gameObject.transform;
                 DameSet();
            }
        }
    }
}
