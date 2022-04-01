using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ZombieManager : MonoBehaviour
{
    //ターゲットオブジェクト
    private GameObject _Target;
    //センサーオブジェクト
    private GameObject _Sensor;
    //アタックヒットオブジェクト
    private GameObject _AttackHit;

    //座標
    private Vector3 _position;
    //回転
    private Vector3 _angle;
    //ターゲット座標
    private Vector3 _target_position;
    //アニメーター
    private Animator _animator;
    //コライダー
    private BoxCollider _collider;

    //ステータス
    public int _st;
    //バージョン
    private int _ver;
    //タイマー
    private float _timer;
    //ラジアン
    private float _rad;
    //角度
    private float _rot;

    //距離
    //private float _dis;
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
    //_st-4-攻撃

    void Awake()
    {
        _Sensor = transform.Find("Sensor").gameObject;
        _AttackHit = transform.Find("AttackHit").gameObject;

        _position = transform.position;
        _angle = transform.localEulerAngles;
        _animator = GetComponent<Animator>();
        _collider=GetComponent<BoxCollider>(); ;
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


            //2点間の距離
            //_dis = Mathf.Sqrt((_target_position.x - _position.x) * (_target_position.x - _position.x) + (_target_position.z - _position.z) * (_target_position.z - _position.z));

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
        else if (_st==4)
        {
            if (_animator.GetBool("attack_trigger")==false)
            {
                _st = 1;
            }
        }
    }

    public void ActiveSet(int _no)
    {
        _st = 1;
        _timer = 0;
        _ver = _no;
        if (_ver==1) {
            transform.position = new Vector3(3, 0, 3);
        }
        else
        {
            transform.position = new Vector3(5, 0, 3);
        }

        
    }

    void OnTriggerEnter(Collider other)
    {
        if (_st==1||_st==2)
        {
            if (other.gameObject.tag == "BulletHole") {
                other.gameObject.transform.parent = this.gameObject.transform;

                --_life;
                if (_life <= 0)
                {
                    _st = 3;
                    _animator.SetBool("dame_trigger", true);
                    _collider.enabled = false;
                    _Sensor.SetActive(false);
                    _AttackHit.SetActive(false);
                }
            }
            else if (other.gameObject.tag == "Player")
            {
                _timer = 0;
                _st = 1;
                _animator.SetBool("walk_flag", false);

            }
        }
    }

    public void AttackSet()
    {
        _st = 4;
        _animator.SetBool("attack_trigger", true);
    }
}
