using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //回転スクリプト
    private RotateManager _RotateManager;
    //FPSキャラクターオブジェクト
    private GameObject _FPS_Character;
    //FPSキャラクタースクリプト
    private FPS_CharacterManager _FPS_CharacterManager;
    //弾痕プレハブ
    public GameObject _BulletHolePrefab;
    //弾痕オブジェクト
    private GameObject _BulletHole;
    //サブカメラ
    private GameObject _SubCamera;

    //アニメーター
    private Animator _animator;
    //座標
    private Vector3 _position;
    //サブカメラ座標
    private Vector3 _sub_camera_position;
    //サブカメラ正面角度
    private Vector3 _sub_camera_forword_angle;

    //ステータス
    private int _st;
    //移動スピード
    public float _speed;
    //移動量X
    private float _vx;
    //移動量Z
    private float _vz;

    //_st=1-基本形
    //_st=2-移動
    //_st=3-ショット

    void Awake()
    {
        _RotateManager = GetComponent<RotateManager>();
        _FPS_Character = transform.Find("FPS_Character").gameObject;
        _FPS_CharacterManager = _FPS_Character.GetComponent<FPS_CharacterManager>();
        _SubCamera = transform.Find("SubCamera").gameObject;

        _animator = GetComponent<Animator>();
        _position = transform.position;
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
            if (_st==1)
            {
                _st = 3;
                _animator.Play("Character_AimPose");
                _FPS_CharacterManager.ShootSet();

                BulletHoleSet();
            }
            _RotateManager.ShootRot();
        }
    }

    void FixedUpdate()
    {
        if (_st==1)
        {
            if (_vx!=0||_vz!=0)
            {
                _st = 2;
                _animator.Play("Character_Walk");
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
        _animator.Play("Character_Idle");
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
            _BulletHole = Instantiate(_BulletHolePrefab);
            //位置
            _BulletHole.transform.position = hit.point;
            //角度
            _BulletHole.transform.rotation = Quaternion.FromToRotation(Vector3.forward, hit.normal);
        }
    }
}
