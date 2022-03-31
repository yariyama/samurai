using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ZombieManager : MonoBehaviour
{
    //ターゲットオブジェクト
    private GameObject _Target;

    //座標
    private Vector3 _position;
    //回転
    private Vector3 _angle;
    //ターゲット座標
    private Vector3 _target_position;
    //アニメーター
    private Animator _animator;

    //ステータス
    private int _st;
    //バージョン
    private int _ver;
    //タイマー
    private float _timer;
    //ラジアン
    private float _rad;
    //角度
    private float _rot;

    //距離
    private float _dis_b = 0.5f;

    //x移動量
    private float _vx;
    //z移動量
    private float _vz;

    //ランダム値
    private int _ran;

    //ライフ値
    public int _life=20;

    //_st=1-基本形
    //_st=2-移動
    //_st=3-ダメージ

    void Awake()
    {
        _position = transform.position;
        _angle = transform.localEulerAngles;
        _animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    void FixedUpdate()
    {
        if (_st==1)
        {
            _timer += Time.deltaTime;
            if (_timer>=5)
            {
                _ran = Random.Range(0,10);
                if (_ran==1)
                {
                    _timer = 0;
                    _st = 2;
                    _animator.SetBool("walk_flag", true);

                    _Target = GameObject.Find("Player" + _ver);
                    _target_position = _Target.transform.position;
                }
            }
        }
        else if (_st == 2)
        {

            _position = transform.position;
            _target_position = _Target.transform.position;

            //2点間の角度
            _rad = Mathf.Atan2(_target_position.z - _position.z, _target_position.x - _position.x);
            _rot = _rad * 180 / Mathf.PI;
            if (_rot < 0)
            {
                _rot += 360;
            }
            _angle.y = (_rot + 90) * -1+180;
            transform.localEulerAngles = _angle;

            _vx = Mathf.Cos(_rot * Mathf.PI / 180) * _dis_b;
            _vz = Mathf.Sin(_rot * Mathf.PI / 180) * _dis_b;

            _position.x += _vx / 50;
            _position.z += _vz / 50;
            transform.position = _position;


            _timer += Time.deltaTime;
            if (_timer >= 10)
            {
                _ran = Random.Range(0, 10);
                if (_ran == 1)
                {
                    _timer = 0;
                    _st = 1;
                    _animator.SetBool("walk_flag", false);
                }
            }

        }
    }

    public void ActiveSet(int _no)
    {
        _st = 1;
        _timer = 0;
        _ver = _no;
        transform.position = new Vector3(3,0,3);

        
    }

    void OnTriggerEnter(Collider other)
    {
        if ((_st==1||_st==2) && other.gameObject.tag=="BulletHole")
        {
            other.gameObject.transform.parent = this.gameObject.transform;

            --_life;
            if (_life<=0)
            {
                _st = 3;
                _animator.SetBool("dame_trigger", true);
            }
        }
    }
}
